namespace ShopSphere.App.Domain;

public class Address : EntityBase
{
    public required string LineOne { get; set; }
    public required string Location { get; set; }

    public required ICollection<Order> Orders { get; set; } = new List<Order>();

}
