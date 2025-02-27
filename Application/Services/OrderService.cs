using shop_api.Domain.Entities;
using shop_api.Infrastructure.Repositories;

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
    
    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _orderRepository.GetOrdersAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetOrderByIdAsync(id);    
    }
    
    public async Task AddProductToOrderAsync(int orderId, int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        
        if (product is null)
            throw new Exception();
        
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
             
        if (order is null)
            throw new Exception();
        
        order.Products.Add(product);
    }

    public async Task FinishOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
        if (order is null || order.Products.Count == 0)
            throw new Exception();
        
        await _orderRepository.AddOrderAsync(order);
    }

    public async Task RemoveProductFromOrderAsync(int orderId, int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        
        if (product is null)
            throw new Exception();
        
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
        if (order is null)
            throw new Exception();
        
        order.Products.Remove(product);
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        
        if (order is null)
            throw new Exception();

        await _orderRepository.DeleteOrderAsync(orderId);
    }
}

