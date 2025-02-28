using Microsoft.AspNetCore.Mvc;
using shop_api.Application.Services;

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
        var result = await _orderService.GetOrdersAsync();
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(
        [FromRoute] int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{orderId}/product/{productId}")]
    public async Task<IActionResult> AddProductToOrderAsync(
        [FromRoute] int orderId,
        [FromRoute] int productId)
    {
        var result = await _orderService.AddProductToOrderAsync(orderId, productId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("finish/{orderId}")]
    public async Task<IActionResult> FinishOrderAsync(
        [FromRoute] int orderId)
    {
        var result = await _orderService.FinishOrderAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{orderId}/product/{productId}")]
    public async Task<IActionResult> RemoveProductFromOrderAsync(
        [FromRoute] int orderId,
        [FromRoute] int productId)
    {
        var result = await _orderService.RemoveProductFromOrderAsync(orderId, productId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrderAsync(
        [FromRoute] int orderId)
    {
        var result = await _orderService.DeleteOrderAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }
}