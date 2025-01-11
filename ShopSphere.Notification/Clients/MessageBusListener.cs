using Microsoft.Extensions.Configuration;
using ShopSphere.Notification.Events;

namespace ShopSphere.Notification.Clients;

public class MessageBusListener : MessageBusListenerBase
{
    public MessageBusListener(IConfiguration config, IEventProcessor eventProcessor) : base(config, eventProcessor)
    {
    }
}