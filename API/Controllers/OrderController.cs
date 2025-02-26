using Microsoft.AspNetCore.Mvc;
using shop_api.Application.Services;
using shop_api.Domain.Entities;

namespace shop_api.API.Controllers;

[ApiController]
[Route("api/controller")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    
    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _orderService.GetOrders();
        return Ok(orders);
    }
}