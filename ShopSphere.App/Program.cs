using Microsoft.EntityFrameworkCore;
using ShopSphere.App.Clients;
using ShopSphere.App.Data;
using ShopSphere.App.Repos;
using ShopSphere.App.Service;

var builder = WebApplication.CreateBuilder(args);

// DbContext - memory if not prod, sql server if prod
if (!builder.Environment.IsProduction())
{
    Console.WriteLine("Using in memory db");
    builder.Services.AddDbContext<ShopSphereDbContext>(options =>
    {
        options.UseInMemoryDatabase("InMemoryDB");
    });
}
else
{
    var connString = builder.Configuration.GetConnectionString("DbConnStr");
    Console.WriteLine("Using in sql server - " + connString);

    builder.Services.AddDbContext<ShopSphereDbContext>(options =>
    {
        options.UseSqlServer(connString);
    });
}

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// single MB client for entire app lifecycle
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

// Repo
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

// Services
builder.Services.AddScoped<IOrderService, OrderService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=Index}/{id?}");

// autopopulate db
DbInitializer.PopulateDb(app);

app.Run();
