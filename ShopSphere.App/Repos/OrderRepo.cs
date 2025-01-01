using Microsoft.EntityFrameworkCore;
using ShopSphere.App.Data;
using ShopSphere.App.Domain;

namespace ShopSphere.App.Repos;

public interface IOrderRepo {
    Task<List<Order>> GetOrdersForUser(int userId);
    Task<Order> Get(int orderId);
    Task<List<Order>> Get();
    Task<Order> Create(Order order) ;
}

public class OrderRepo : RepoBase, IOrderRepo
{

    public OrderRepo(ShopSphereDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get all orders (remove after able to login and be in context of a user)
    /// </summary>
    /// <returns></returns>
    public async Task<List<Order>> Get()
    {
        var orders = await _context.Orders
            .Include(o => o.ShippingAddress)
            .Include(o => o.User)
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
            .Include(o => o.User)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        return orders; 
    }

    public async Task<Order> Create(Order order) 
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return order;
    }

}
