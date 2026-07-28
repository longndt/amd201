using AuthService.Data;
using AuthService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Services (dependency injection container) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core with SQL Server. Connection string comes from configuration/env vars.
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<TokenService>();

// CORS: allow the React frontend to call this API from the browser.
var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                     ?? new[] { "http://localhost:5173" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Create the database/tables from the EF model on startup if they don't exist.
// (Simple and self-contained for a lab. Production apps use EF migrations instead.)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    var retries = 10;
    while (true)
    {
        try { db.Database.EnsureCreated(); break; }
        catch when (retries-- > 0)
        {
            // SQL Server in Docker may still be starting up — wait and retry.
            Console.WriteLine("Waiting for the database to be ready...");
            Thread.Sleep(3000);
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("frontend");
app.MapControllers();

// Simple health check endpoint (used by Docker / Render).
app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "auth" }));

app.Run();
