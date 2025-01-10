using ShopSphere.Services.Core.MessageBus.Events;
using ShopSphere.Services.Core.MessageBus.Models;

namespace ShopSphere.Notification.Events;

public class EventProcessor : IEventProcessor
{
    public void Process(Message message)
    {
        Console.WriteLine("Event: " + message.Event);
        throw new NotImplementedException();
    }
}
