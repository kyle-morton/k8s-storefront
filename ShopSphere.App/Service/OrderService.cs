using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopSphere.App.Clients;
using ShopSphere.App.Clients.Models;
using ShopSphere.App.Domain;
using ShopSphere.App.Repos;

namespace ShopSphere.App.Service;

public interface IOrderService
{
    Task<List<Order>> GetOrdersForUser(int userId);
    Task<Order> Get(int orderId);
    Task<List<Order>> Get();
    Task<Order> Create(Order order);
    Task CreateMockOrder();
}

public class OrderService : IOrderService
{
    private IOrderRepo _repo;
    private IUserRepo _userRepo;
    private readonly IMessageBusClient _messageBusClient;
    private IMapper _mapper;

    public OrderService(IOrderRepo repo, IUserRepo userRepo, IMessageBusClient messageBusClient, IMapper mapper)
    {
        _repo = repo;
        _userRepo = userRepo;
        _messageBusClient = messageBusClient;
        _mapper = mapper;
    }

    public async Task<List<Order>> GetOrdersForUser(int userId)
    {
        return await _repo.GetOrdersForUser(userId);
    }

    public async Task<Order> Get(int orderId)
    {
        return await _repo.Get(orderId);
    }

    public async Task<List<Order>> Get()
    {
        return await _repo.Get();
    }

    public async Task<Order> Create(Order order)
    {
        order = await _repo.Create(order);

        // publish to messageBus
        var createdOrder = _mapper.Map<CreatedOrder>(order);
        _messageBusClient.Publish(createdOrder, "Order-Created");

        return order;
    }

    public async Task CreateMockOrder()
    {
        var user = (await _userRepo.Get()).FirstOrDefault();
        var order = new Order
        {
            Total = new Random().Next(50, 300),
            Items = "Test Supplies",
            ShippingAddress = new Address
            {
                LineOne = "123 Main Street",
                Location = "Paragould, AR 72450",
                Orders = new List<Order>()
            },
            UserId = user.Id
        };

        await Create(order);
    }
}
