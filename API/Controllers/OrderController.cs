using Microsoft.AspNetCore.Mvc;
using shop_api.Application.Services;
using shop_api.Domain.Entities;

namespace shop_api.API.Controllers;

[ApiController]
[Route("api/order")]
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var orders = await _orderService.GetOrderById(id);
        return Ok(orders);
    }

    [HttpPost("product/{orderId}")]
    public async Task<IActionResult> AddProductToOrder(
        [FromRoute] int orderId,
        [FromBody] Product product)
    {
        await _orderService.AddProduct(orderId, product);
        return Ok();
    }
    
    [HttpPost("finish/{orderId}")]
    public async Task<IActionResult> FinishOrder(
        [FromRoute] int orderId)
    {
        await _orderService.FinishOrder(orderId);
        return Ok();
    }
}