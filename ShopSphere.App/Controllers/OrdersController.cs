using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopSphere.App.Repos;
using ShopSphere.App.ViewModels.Orders;

namespace ShopSphere.App.Controllers;

[Route("[controller]")]
public class OrdersController : Controller
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepo _repo;

    public OrdersController(ILogger<OrdersController> logger, IOrderRepo repo, IMapper mapper)
    {
        _repo = repo;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<IActionResult> Index()
    {
        var orders = await _repo.Get();
        var orderVMs = _mapper.Map<List<OrderViewModel>>(orders);

        return View(new IndexViewModel { Orders = orderVMs });
    }
}
