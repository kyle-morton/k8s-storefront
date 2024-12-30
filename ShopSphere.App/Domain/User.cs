using System.ComponentModel.DataAnnotations;

namespace ShopSphere.App.Domain;

public class User : EntityBase
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Name { get; set; }

    public required ICollection<Order> Orders { get; set; } = new List<Order>();

}
