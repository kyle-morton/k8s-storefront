﻿using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopSphere.Services.Core.MessageBus.Events;
using ShopSphere.Services.Core.MessageBus.Models;

namespace ShopSphere.Services.Core.MessageBus;

public abstract class MessageBusListenerBase : BackgroundService
{

    private readonly string _host;
    private readonly int _port;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusListenerBase(IConfiguration config, IEventProcessor eventProcessor)
    {
        _host = config["RabbitMQ:Host"];
        _port = int.Parse(config["RabbitMQ:Port"]);

        Console.WriteLine($"Config: {_host}:{_port.ToString()}");

        _eventProcessor = eventProcessor;

        InitMessageBusConnection();
    }

    #region INIT/DISPOSE CONNECTION

    private void InitMessageBusConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _host,
            Port = _port
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

            var eventStr = Encoding.UTF8.GetString(ea.Body.ToArray());

            Console.WriteLine($"MessageBusSubscriber: Event Body - {eventStr}");
            var message = JsonSerializer.Deserialize<Message>(eventStr);

            _eventProcessor.Process(message);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}

