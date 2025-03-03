// using Microsoft.AspNetCore.Mvc;
// using shop_api.Application.Services;
// using shop_api.Common;
// using shop_api.Domain.Entities;
//
// namespace shop_api.API.Controllers;
//
// [ApiController]
// [Route("api/order")]
// public class OrderController : ControllerBase
// {
//     private readonly OrderService _orderService;
//
//     public OrderController(OrderService orderService)
//     {
//         _orderService = orderService;
//     }
//
//     [HttpGet]
//     public async Task<IActionResult> GetOrdersAsync()
//     {
//         var result = await _orderService.GetOrdersAsync();
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         var orders = (List<Order>)result.GetResult();
//         return Ok(orders);
//     }
//
//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetOrderByIdAsync(
//         [FromRoute] int id)
//     {
//         var result = await _orderService.GetOrderByIdAsync(id);
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         var order = (Order)result.GetResult();
//         return Ok(order);
//     }
//
//     [HttpPost]
//     public async Task<IActionResult> CreateOrderAsync(
//         [FromBody] Order order)
//     {
//         var result = await _orderService.CreateOrderAsync(order);
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         var createdOrder = (Order)result.GetResult();
//         return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = createdOrder.Id }, createdOrder);
//     }
//
//     [HttpPost("{orderId}/product/{productId}")]
//     public async Task<IActionResult> AddProductToOrderAsync(
//         [FromRoute] int orderId,
//         [FromRoute] int productId)
//     {
//         var result = await _orderService.AddProductToOrderAsync(orderId, productId);
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         return Ok(result);
//     }
//
//     [HttpPost("{orderId}/finish")]
//     public async Task<IActionResult> FinishOrderAsync(
//         [FromRoute] int orderId)
//     {
//         var result = await _orderService.FinishOrderAsync(orderId);
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         return Ok(result);
//     }
//
//     [HttpDelete("{orderId}/product/{productId}")]
//     public async Task<IActionResult> RemoveProductFromOrderAsync(
//         [FromRoute] int orderId, 
//         [FromRoute] int productId)
//     {
//         var result = await _orderService.RemoveProductFromOrderAsync(orderId, productId);
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         return Ok(result);
//     }
//
//     [HttpDelete("{orderId}")]
//     public async Task<IActionResult> DeleteOrderAsync(
//         [FromRoute] int orderId)
//     {
//         var result = await _orderService.DeleteOrderAsync(orderId);
//
//         if (result is ProblemDetailsResponse)
//         {
//             var problemDetails = (ProblemDetails)result.GetResult();
//             return StatusCode((int)problemDetails.Status, problemDetails);
//         }
//
//         var removedOrder = (Order)result.GetResult();
//         return Ok(removedOrder);
//     }
// }