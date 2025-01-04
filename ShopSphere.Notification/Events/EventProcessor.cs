using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopSphere.Notification.Clients.Models;

namespace ShopSphere.Notification.Events;

public interface IEventProcessor {
    void Process(Message message);
}

public class EventProcessor : IEventProcessor
{
    public void Process(Message message)
    {
        Console.WriteLine("Event: " + message.Event);
        throw new NotImplementedException();
    }
}
