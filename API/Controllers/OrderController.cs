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
    public async Task<IActionResult> GetOrdersAsync()
    {
        var orders = await _orderService.GetOrdersAsync();
        return Ok(orders);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(
        [FromRoute] int id)
    {
        var orders = await _orderService.GetOrderByIdAsync(id);
        return Ok(orders);
    }

    [HttpPost("{orderId}/product/{productId}")]
    public async Task<IActionResult> AddProductToOrderAsync(
        [FromRoute] int orderId,
        [FromRoute] int productId)
    {
        await _orderService.AddProductToOrderAsync(orderId, productId);
        return Ok();
    }
    
    [HttpPost("finish/{orderId}")]
    public async Task<IActionResult> FinishOrderAsync(
        [FromRoute] int orderId)
    {
        await _orderService.FinishOrderAsync(orderId);
        return Ok();
    }

    [HttpDelete("{orderId}/product/{productId}")]
    public async Task<IActionResult> RemoveProductFromOrderAsync(
        [FromRoute] int orderId,
        [FromRoute] int productId)
    {
        await _orderService.RemoveProductFromOrderAsync(orderId, productId);
        return Ok();
    }
    
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrderAsync(
        [FromRoute] int orderId)
    {
        await _orderService.DeleteOrderAsync(orderId);
        return Ok();
    }
}