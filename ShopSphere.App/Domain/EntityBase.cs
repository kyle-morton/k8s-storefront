using System.ComponentModel.DataAnnotations;

namespace ShopSphere.App.Domain;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}
