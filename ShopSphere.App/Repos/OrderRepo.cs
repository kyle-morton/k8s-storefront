using Microsoft.EntityFrameworkCore;
using ShopSphere.App.Data;
using ShopSphere.App.Domain;

namespace ShopSphere.App.Repos;

public interface IOrderRepo {
    Task<List<Order>> GetOrdersForUser(int userId);
    Task<Order> Get(int orderId);
    Task<List<Order>> Get();
}

public class OrderRepo : IOrderRepo
{

    private ShopSphereDbContext _context;

    public OrderRepo(ShopSphereDbContext context) 
    {
        _context = context;
    }

    /// <summary>
    /// Get all orders (remove after able to login and be in context of a user)
    /// </summary>
    /// <returns></returns>
    public async Task<List<Order>> Get()
    {
        var orders = await _context.Orders
            .Include(o => o.ShippingAddress)
            .ToListAsync();

        return orders; 
    }

    public async Task<Order> Get(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.ShippingAddress)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        return order; 
    }

    public async Task<List<Order>> GetOrdersForUser(int userId)
    {
        var orders = await _context.Orders
            .Include(o => o.ShippingAddress)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        return orders; 
    }


}
