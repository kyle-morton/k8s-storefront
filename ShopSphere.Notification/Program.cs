using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// using ConsoleDI.Example;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
// builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
// builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
// builder.Services.AddTransient<ServiceLifetimeReporter>();

using IHost host = builder.Build();



await host.RunAsync();