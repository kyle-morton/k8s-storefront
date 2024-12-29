using Microsoft.EntityFrameworkCore;
using ShopSphere.App.Domain;

namespace ShopSphere.App.Data;

public class ShopSphereDbContext : DbContext
{
    public ShopSphereDbContext(DbContextOptions<ShopSphereDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        modelBuilder
            .Entity<Order>()
            .HasOne(o => o.ShippingAddress)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.ShippingAddressId);
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
