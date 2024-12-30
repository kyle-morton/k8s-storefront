using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.App.ViewModels.Orders;

public class OrderViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ShippingAddressId { get; set; }
    public required string Items { get; set; }
    public decimal Total { get; set; }

    public UserViewModel User { get; set; }
    public AddressViewModel ShippingAddress { get; set; }
}
