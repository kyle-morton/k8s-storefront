using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopSphere.App.Clients;
using ShopSphere.App.Domain;
using ShopSphere.App.Repos;
using ShopSphere.App.ViewModels.Orders;

namespace ShopSphere.App.Controllers;

[Route("")]
public class OrdersController : Controller
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepo _orderRepo;
    private readonly IUserRepo _userRepo;
    private readonly IMessageBusClient _messageBusClient;

    public OrdersController(ILogger<OrdersController> logger, IOrderRepo orderRepo, IUserRepo userRepo, IMapper mapper, IMessageBusClient messageBusClient)
    {
        _orderRepo = orderRepo;
        _userRepo = userRepo;
        _logger = logger;
        _mapper = mapper;
        _messageBusClient = messageBusClient;
    }


    public async Task<IActionResult> Index()
    {
        var orders = await _orderRepo.Get();
        var orderVMs = _mapper.Map<List<OrderViewModel>>(orders);

        return View(new IndexViewModel { Orders = orderVMs });
    }

    [Route("Orders/Create", Name = "CreateOrder")]
    public async Task<IActionResult> Create()
    {
        // Mocking this out for now
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
        order = await _orderRepo.Create(order);

        // publish model here - map to a read-only version of the order

        return RedirectToAction("");
    }
}
