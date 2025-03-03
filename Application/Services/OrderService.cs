// using System.Net;
// using Microsoft.AspNetCore.Mvc;
// using shop_api.Common;
// using shop_api.Domain.Entities;
// using shop_api.Infra.Repositories;
//
// namespace shop_api.Application.Services;
//
// public class OrderService
// {
//     private readonly OrderRepository _orderRepository;
//     private readonly ProductRepository _productRepository;
//
//     public OrderService(OrderRepository orderRepository, ProductRepository productRepository)
//     {
//         _orderRepository = orderRepository;
//         _productRepository = productRepository;
//     }
//     
//     public async Task<ICustomResult> GetOrdersAsync()
//     {
//         var orders = await _orderRepository.GetOrdersAsync();
//         
//         return orders.Any() 
//             ? ResponseFactory.Success(orders) 
//             : ResponseFactory.Failure(404, "Orders are empty");
//     }
//
//     public async Task<ICustomResult> GetOrderByIdAsync(int id)
//     {
//         var order = await _orderRepository.GetOrderByIdAsync(id);
//
//         return order is null
//             ? ResponseFactory.Failure(404, "Order not found")
//             : ResponseFactory.Success(order);
//     }
//     
//     public async Task<ICustomResult> CreateOrderAsync(Order order)
//     {
//         await _orderRepository.AddOrderAsync(order);
//         return ResponseFactory.Success(order);
//     }
//     
//     public async Task<ICustomResult> AddProductToOrderAsync(int orderId, int productId)
//     {
//         var order = await _orderRepository.GetOrderByIdAsync(orderId);
//         
//         if (order is null) 
//             return ResponseFactory.Failure(404, "Order not found");
//         
//         var product = await _productRepository.GetProductByIdAsync(productId);
//         
//         if (product is null) 
//             return ResponseFactory.Failure(404, "Product not found");
//         
//         order.Products.Add(product);
//         await _orderRepository.UpdateOrderAsync(order);
//         
//         return ResponseFactory.Success(order);
//     }
//     
//     public async Task<ICustomResult> RemoveProductFromOrderAsync(int orderId, int productId)
//     {
//         var order = await _orderRepository.GetOrderByIdAsync(orderId);
//         
//         if (order is null) 
//             return ResponseFactory.Failure(404, "Order not found");
//         
//         var product = order.Products.FirstOrDefault(p => p.Id == productId);
//         
//         if (product is null) 
//             return ResponseFactory.Failure(400, "Product not found in order");
//         
//         order.Products.Remove(product);
//         await _orderRepository.UpdateOrderAsync(order);
//         
//         return ResponseFactory.Success(order);
//     }
//     
//     public async Task<ICustomResult> FinishOrderAsync(int orderId)
//     {
//         var order = await _orderRepository.GetOrderByIdAsync(orderId);
//         
//         if (order is null)
//             return ResponseFactory.Failure(404, "Order not found");
//         
//         order.IsCompleted = true;
//         await _orderRepository.UpdateOrderAsync(order);
//         
//         return ResponseFactory.Success(order);
//     }
//     
//     public async Task<ICustomResult> DeleteOrderAsync(int orderId)
//     {
//         var order = await _orderRepository.GetOrderByIdAsync(orderId);
//         
//         if (order is null) 
//             return ResponseFactory.Failure(404, "Order not found");
//         
//         await _orderRepository.DeleteOrderAsync(order);
//         return ResponseFactory.Success(order);
//     }
// }
//
