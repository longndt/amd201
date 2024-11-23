using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile(
  "gateway.json",
  optional: false,
  reloadOnChange: true
  );

builder.Services.AddOcelot(builder.Configuration);

//(1) config CORS   => VERY IMPORTANT
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

//(2) enable CORS    => VERY IMPORTANT
app.UseCors("CorsPolicy");

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseOcelot().Wait();

app.Run();