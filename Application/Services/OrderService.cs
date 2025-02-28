using System.Net;
using Microsoft.AspNetCore.Mvc;
using shop_api.API.Common;
using shop_api.Domain.Entities;
using shop_api.Infra.Repositories;

namespace shop_api.Application.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly ProductRepository _productRepository;

    public OrderService(
        OrderRepository orderRepository,
        ProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }
    
    public async Task<Result<IEnumerable<Order>>> GetOrdersAsync()
    {
        try
        {
            var orders = await _orderRepository.GetOrdersAsync();
            
            return Result<IEnumerable<Order>>
                .Success(StatusCodes.Status200OK, "Order fetched successfully", orders);
        }
        catch (RepositoryException ex)
        {
            return Result<IEnumerable<Order>>
                .Failure(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    public async Task<Result<Order>> GetOrderByIdAsync(int id)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            return order is null
                ? Result<Order>.Failure(StatusCodes.Status404NotFound, "Order not found")
                : Result<Order>.Success(StatusCodes.Status200OK, "Order fetched successfully", order);
        }
        catch (RepositoryException ex)
        {
            return Result<Order>
                .Failure(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    public async Task<Result<Product>> AddProductToOrderAsync(int orderId, int productId)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (order is null)
                return Result<Product>.Failure(StatusCodes.Status404NotFound, "Order not found");
        
            if (product is null)
                return Result<Product>.Failure(StatusCodes.Status404NotFound, "Product not found");
        
            order.Products.Add(product);
            return Result<Product>.Success(StatusCodes.Status201Created, $"Product added to order with ID {orderId}", product);
        }
        catch (RepositoryException ex)
        {
            return Result<Product>
                .Failure(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    public async Task<Result<Order>> FinishOrderAsync(int orderId)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
            if (order is null)
                return Result<Order>.Failure(StatusCodes.Status404NotFound, "Order not found");
        
            if (order.Products.Count.Equals(0))
                return Result<Order>.Failure(StatusCodes.Status400BadRequest, "Cannot finish an empty order");
        
            await _orderRepository.AddOrderAsync(order);
            return Result<Order>.Success(StatusCodes.Status201Created, "Order created successfully", order);
        }
        catch (RepositoryException ex)
        {
            return Result<Order>
                .Failure(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    public async Task<Result<Product>> RemoveProductFromOrderAsync(int orderId, int productId)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            var product = await _productRepository.GetProductByIdAsync(productId);
        
            if (order is null)
                return Result<Product>.Failure(StatusCodes.Status404NotFound, "Order not found");
        
            if (product is null)
                return Result<Product>.Failure(StatusCodes.Status404NotFound, "Product not found");
        
            order.Products.Remove(product);
            return Result<Product>.Success(StatusCodes.Status200OK, $"Product removed from order with ID {orderId}", product);
        }
        catch (RepositoryException ex)
        {
            return Result<Product>
                .Failure(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    public async Task<Result<Order>> DeleteOrderAsync(int orderId)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
            if (order is null)
                return Result<Order>.Failure(StatusCodes.Status404NotFound, "Order not found");

            await _orderRepository.DeleteOrderAsync(orderId);
            return Result<Order>.Success(StatusCodes.Status200OK, "Order deleted successfully", order);
        }
        catch (RepositoryException ex)
        {
            return Result<Order>
                .Failure(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

