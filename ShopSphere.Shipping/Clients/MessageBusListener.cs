using Microsoft.Extensions.Configuration;
using ShopSphere.Shipping.Events;

namespace ShopSphere.Shipping.Clients;

public class MessageBusListener : MessageBusListenerBase
{
    public MessageBusListener(IConfiguration config, IEventProcessor eventProcessor) : base(config, eventProcessor)
    {
    }
}