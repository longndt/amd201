using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile(
  "ocelot.json",
  optional: false,
  reloadOnChange: true
  );

builder.Services.AddOcelot(builder.Configuration).AddCacheManager(x =>
{
    x.WithDictionaryHandle();
});

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseOcelot().Wait();


app.Run();