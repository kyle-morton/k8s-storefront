using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopSphere.App.Clients;
using ShopSphere.App.Clients.Models;
using ShopSphere.App.Domain;
using ShopSphere.App.Repos;
using ShopSphere.App.Service;
using ShopSphere.App.ViewModels.Orders;

namespace ShopSphere.App.Controllers;

[Route("")]
public class OrdersController : Controller
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService, IMapper mapper, IMessageBusClient messageBusClient)
    {
        _orderService = orderService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.Get();
        var orderVMs = _mapper.Map<List<OrderViewModel>>(orders);

        return View(new IndexViewModel { Orders = orderVMs });
    }

    [Route("Orders/Create", Name = "CreateOrder")]
    public async Task<IActionResult> Create()
    {
        await _orderService.CreateMockOrder();

        return RedirectToAction("");
    }
}
