using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.App.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
}
