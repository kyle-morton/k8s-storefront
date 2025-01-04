using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopSphere.Notification.Events;

public interface IEventProcessor {
    void Process(string evt);
}

public class EventProcessor : IEventProcessor
{
    public void Process(string evt)
    {
        Console.WriteLine("Event: " + evt);
        throw new NotImplementedException();
    }
}
