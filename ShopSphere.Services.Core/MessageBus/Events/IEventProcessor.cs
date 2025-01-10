using ShopSphere.Services.Core.MessageBus.Models;

namespace ShopSphere.Services.Core.MessageBus.Events;

public interface IEventProcessor
{
    void Process(Message message);
}
