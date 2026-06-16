using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Register MVC
builder.Services.AddControllersWithViews();

// Register services in DI container
// Transient: new instance every resolution
builder.Services.AddTransient<IGreeter, GreeterService>();
// Scoped: one per HTTP request
builder.Services.AddScoped<IClock, SystemClock>();
// Singleton: one for the whole app lifetime
builder.Services.AddSingleton<IAppId, AppIdSingleton>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// ======== Contracts & Implementations =========
public interface IGreeter
{
    string Greet(string name);
}

public interface IClock
{
    DateTime Now();
}

public interface IAppId
{
    Guid Id { get; }
}

public class GreeterService : IGreeter
{
    private readonly IClock _clock;
    private readonly IAppId _appId;

    // Constructor injection of other services
    public GreeterService(IClock clock, IAppId appId)
    {
        _clock = clock;
        _appId = appId;
    }

    public string Greet(string name)
    {
        // Example: combining multiple dependencies
        return $"Hello {name}! Time: {_clock.Now():O} | AppId: {_appId.Id}";
    }
}

public class SystemClock : IClock
{
    // Scoped: changes per request if needed (e.g., request culture/timezone)
    public DateTime Now() => DateTime.UtcNow;
}

public class AppIdSingleton : IAppId
{
    // Singleton: stable across the entire app lifetime
    public Guid Id { get; } = Guid.NewGuid();
}
