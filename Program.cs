using DependencyInjection.Interfaces;
using DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);
/*
There are three 'scopes' for adding a dependency:
- Singleton: Service is available for the lifetime of the app, useful for e.g. reading configuration files at runtime
- Scoped: Service is available for the lifetime of the request, useful for e.g. database connections
- Transient: Service is created each time it is requested, useful for e.g. services that are stateless
*/
builder.Services.AddScoped<IWelcomeService, WelcomeService>();

var app = builder.Build();

app.MapGet("/", async (IWelcomeService service1, IWelcomeService service2) =>
{
    var message1 = service1.GetWelcomeMessage();
    var message2 = service2.GetWelcomeMessage();
    return Results.Text($"Service 1: {message1}\nService 2: {message2}");
});

app.Run();
