namespace ShopSphere.Notification.Clients.Models;

public class Message
{
    public required object Payload { get; set; }
    public required string Event { get; set; }
}
