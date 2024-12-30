using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.App.ViewModels;

public class AddressViewModel
{
    public int Id { get; set; }
    public required string LineOne { get; set; }
    public required string Location { get; set; }
}
