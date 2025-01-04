namespace ShopSphere.App.Clients.Models;

public class CreatedOrder
{
    public int UserId { get; set; }
    public int ShippingAddressId { get; set; }
    public required string Items { get; set; }
    public decimal Total { get; set; }

    public CreatedOrderUser User { get; set; }
    public CreatedOrderAddress ShippingAddress { get; set; }
}
