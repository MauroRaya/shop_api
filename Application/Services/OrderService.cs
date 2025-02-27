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
    
    public async Task<List<Order>> GetOrders()
    {
        return await _orderRepository.GetOrders();
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await _orderRepository.GetOrderById(id);    
    }
    
    public async Task AddProduct(int orderId, Product product)
    {
        var products = await _productRepository.GetProducts();
        
        if (!products.Contains(product))
            throw new Exception();
        
        var order = await _orderRepository.GetOrderById(orderId);
             
        if (order is null)
            throw new Exception();
        
        order.Products.Add(product);
    }

    public async Task FinishOrder(int orderId)
    {
        var order = await _orderRepository.GetOrderById(orderId);
        
        if (order is null || order.Products.Count == 0)
            throw new Exception();
        
        await _orderRepository.AddOrder(order);
    }
}

