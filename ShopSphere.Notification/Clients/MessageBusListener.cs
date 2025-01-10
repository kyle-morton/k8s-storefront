using Microsoft.Extensions.Configuration;
using ShopSphere.Services.Core.MessageBus;
using ShopSphere.Services.Core.MessageBus.Events;

namespace ShopSphere.Notification.Clients.Models;

public class MessageBusListener : MessageBusListenerBase
{
    public MessageBusListener(IConfiguration config, IEventProcessor eventProcessor) : base(config, eventProcessor)
    {
    }
}