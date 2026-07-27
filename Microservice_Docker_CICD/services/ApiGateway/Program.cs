using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Pick the routing table that matches the current environment:
//   Development (local dotnet run) -> ocelot.json           (localhost:7001 / 7002)
//   Docker (docker compose)        -> ocelot.Docker.json    (authservice / productservice)
//   Production (Render)            -> ocelot.Production.json (public https URLs)
var ocelotFile = builder.Environment.EnvironmentName switch
{
    "Docker" => "ocelot.Docker.json",
    "Production" => "ocelot.Production.json",
    _ => "ocelot.json"
};
builder.Configuration.AddJsonFile(ocelotFile, optional: false, reloadOnChange: true);

// Let environment variables override the routing table (handy on Render, so you can
// point the gateway at your deployed services WITHOUT rebuilding the image), e.g.:
//   Routes__0__DownstreamHostAndPorts__0__Host = lab-authservice.onrender.com
//   Routes__1__DownstreamHostAndPorts__0__Host = lab-productservice.onrender.com
//   Routes__2__DownstreamHostAndPorts__0__Host = lab-productservice.onrender.com
builder.Configuration.AddEnvironmentVariables();

// The browser talks ONLY to this gateway, so CORS is configured here.
var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                     ?? new[] { "http://localhost:7173", "http://localhost:7080" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Gateway health check (branch off before Ocelot's terminal middleware).
app.Map("/health", b => b.Run(async ctx =>
{
    ctx.Response.ContentType = "application/json";
    await ctx.Response.WriteAsync("{\"status\":\"healthy\",\"service\":\"gateway\"}");
}));

app.UseCors("frontend");

// Ocelot forwards each request (including the Authorization header) to the
// matching downstream service defined in the ocelot.*.json file.
await app.UseOcelot();

app.Run();
