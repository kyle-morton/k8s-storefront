using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.Shipping.Clients.Models;

public class CreatedOrderAddress
{
    public required string LineOne { get; set; }
    public required string Location { get; set; }
}
