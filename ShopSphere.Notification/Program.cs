using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopSphere.Notification.Clients.Models;
using ShopSphere.Notification.Events;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Environment: {environmentName}");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// message bus DI
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

// runs in the background during lifetime of app
builder.Services.AddHostedService<MessageBusListener>();

using IHost host = builder.Build();

await host.RunAsync();