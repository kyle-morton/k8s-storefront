using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopSphere.Shipping.Clients;
using ShopSphere.Shipping.Clients.Models;
using ShopSphere.Shipping.Events;

// Run locally in Development -> dotnet run --environment Development

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var env = builder.Environment;
Console.WriteLine($"Environment: {env}");

List<KeyValuePair<string,string?>> inMemoryConfig;
if (env.IsProduction()){
    inMemoryConfig = new List<KeyValuePair<string, string?>>
    {
        new KeyValuePair<string, string?>("RabbitMQ:Host", "rabbitmq-clusterip-srv"),
        new KeyValuePair<string, string?>("RabbitMQ:Port", "5672")
    };
}
else {
    inMemoryConfig = new List<KeyValuePair<string, string?>>
    {
        new KeyValuePair<string, string?>("RabbitMQ:Host", "localhost"),
        new KeyValuePair<string, string?>("RabbitMQ:Port", "5672")
    };
}

builder.Configuration.AddInMemoryCollection(initialData: inMemoryConfig);

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// message bus DI
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

// runs in the background during lifetime of app
builder.Services.AddHostedService<MessageBusListener>();

using IHost host = builder.Build();

await host.RunAsync();