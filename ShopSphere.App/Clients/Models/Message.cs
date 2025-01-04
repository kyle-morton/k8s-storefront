using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.App.Clients.Models;

public class Message<T>
{
    public required T Payload { get; set; }
    public required string Event { get; set; }
}
