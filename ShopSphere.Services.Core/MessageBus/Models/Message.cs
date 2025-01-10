using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.Services.Core.MessageBus.Models;

public class Message
{
    public required object Payload { get; set; }
    public required string Event { get; set; }
}
