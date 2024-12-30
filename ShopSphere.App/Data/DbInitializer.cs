using Microsoft.EntityFrameworkCore;
using ShopSphere.App.Domain;

namespace ShopSphere.App.Data;

public static class DbInitializer
{

    public static void PopulateDb(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetService<ShopSphereDbContext>();
        Seed(context, app.Environment.IsProduction());
    }

    private static void Seed(ShopSphereDbContext context, bool isProd)
    {
        // run migs if running against sql server
        if (isProd)
        {
            Console.WriteLine("PrepDb: Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PrepDb: unable to apply migrations {ex}");
                throw;
            }
        }


        if (context == null)
        {
            Console.WriteLine("PrepDb: context null");
            return;
        }
        if (context.Users.Any())
        {
            Console.WriteLine("PrepDb: Data exists, skipping populate");
            return;
        }

        Console.WriteLine("PrepDb: seeding data");
        context.Users.AddRange(new List<User> {
            new User { 
                Name = "Kyle Morton", 
                Email = "test@test.com",
                Orders = new List<Order> {
                    new Order {
                        Total = 157.25m,
                        Items = "Books",
                        ShippingAddress = new Address {
                            LineOne = "123 Main Street",
                            Location = "Paragould, AR 72450",
                            Orders = new List<Order>()
                        }
                    },
                    new Order {
                        Total = 24.07m,
                        Items = "Office Supplies",
                        ShippingAddress = new Address {
                            LineOne = "123 Main Street",
                            Location = "Paragould, AR 72450",
                            Orders = new List<Order>()
                        }
                    }
                }
            },
            new User { 
                Name = "Ted Sheckler", 
                Email = "ted@test.com",
                Orders = new List<Order> {
                    new Order {
                        Total = 8.28m,
                        Items = "Funyuns",
                        ShippingAddress = new Address {
                            LineOne = "123 Main Street",
                            Location = "Chicago, IL 60606",
                            Orders = new List<Order>()
                        }
                    },
                    new Order {
                        Total = 100.00m,
                        Items = "Gift Card",
                        ShippingAddress = new Address {
                            LineOne = "123 Main Street",
                            Location = "Chicago, IL 60606",
                            Orders = new List<Order>()
                        }
                    }
                }
            }

        });

        context.SaveChanges();
    }

}