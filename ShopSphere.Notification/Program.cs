using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopSphere.Notification.Clients.Models;
using ShopSphere.Notification.Events;

// Run locally in Development -> dotnet run --environment Development

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Environment: {environmentName}");
Console.WriteLine($"CurrentDir: {Environment.CurrentDirectory}");

builder.Configuration
    .SetBasePath(Environment.CurrentDirectory)
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// message bus DI
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

// runs in the background during lifetime of app
builder.Services.AddHostedService<MessageBusListener>();

using IHost host = builder.Build();

await host.RunAsync();