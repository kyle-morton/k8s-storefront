using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using ShopSphere.App.Clients.Models;

namespace ShopSphere.App.Clients;

public interface IMessageBusClient
{
    void Publish<T>(T model, string eventName);
}

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _config;
    private readonly IConnection? _connection;
    private readonly IModel? _channel;

    public MessageBusClient(IConfiguration config)
    {
        _config = config;

        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ:Host"],
            Port = int.Parse(_config["RabbitMQ:Port"])
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("MessageBusClient: Connected to MessageBus");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"MessageBusClient: Could not connect to the Message Bus: {ex.Message}");
        }
    }

    #region PUBLISH

    public void Publish<T>(T model, string eventName)
    {
        var message = new Message<T>
        {
            Payload = model,
            Event = eventName
        };

        var messageJson = JsonSerializer.Serialize(message);

        if (_connection.IsOpen)
        {
            Console.WriteLine("MessageBusClient: RabbitMQ Connection Open, sending message...");
            SendMessage(messageJson);
        }
        else
        {
            Console.WriteLine("MessageBusClient: RabbitMQ connectionis closed, not sending");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger",
                        routingKey: "",
                        basicProperties: null,
                        body: body);
        Console.WriteLine($"MessageBusClient: We have sent {message}");
    }

    #endregion 

    #region DISPOSE CONNECTION

    public void Dispose()
    {
        Console.WriteLine("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("MessageBusClient: RabbitMQ Connection Shutdown");
    }

    #endregion
}
