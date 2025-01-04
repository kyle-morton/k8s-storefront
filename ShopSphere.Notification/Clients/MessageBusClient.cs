using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopSphere.Notification.Events;

namespace ShopSphere.Notification.Clients.Models;

public class MessageBusClient : BackgroundService
{

    private readonly IConfiguration _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusClient(IConfiguration config, IEventProcessor eventProcessor)
    {
        _config = config;
        Console.WriteLine($"Config: {_config["RabbitMQ:Host"]}:{_config["RabbitMQ:Port"]}");


        _eventProcessor = eventProcessor;

        InitMessageBusConnection();
    }

    #region INIT/DISPOSE CONNECTION

    private void InitMessageBusConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ:Host"],
            Port = int.Parse(_config["RabbitMQ:Port"])
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(
            queue: _queueName,
            exchange: "trigger",
            routingKey: ""
        );

        Console.WriteLine("MessageBusSubscriber: Listening on message bus...");

        _connection.ConnectionShutdown += ConnectionShutdown;
    }

    private void ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("MessageBusSubscriber: Connection shutdown...");
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }

        base.Dispose();
    }


    #endregion

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("MessageBusSubscriber: Execute async running...");

        stoppingToken.ThrowIfCancellationRequested();

        // create connection in init, start listening for messages here
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ModuleHandle, ea) => 
        {
            Console.WriteLine("MessageBusSubscriber: Event received!");

            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            _eventProcessor.Process(notificationMessage);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
