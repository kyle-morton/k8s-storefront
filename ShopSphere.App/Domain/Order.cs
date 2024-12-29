using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopSphere.App.Domain;

public class Order : EntityBase 
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int ShippingAddressId { get; set; }
    [Required]
    public required string Items { get; set; }
    [Required]
    public decimal Total { get; set; }


    public virtual User User { get; set; }
    public virtual Address ShippingAddress { get; set; }

}
