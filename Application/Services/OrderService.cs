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
    
    public async Task AddProduct(Order order, Product product)
    {
        
    }

    public async Task FinishShopping(Order order)
    {
        
    }
    
    public async Task UpdateOrder(Order order)
    {
        
    }
    
    public async Task RemoveOrder(Order order)
    {
        
    }
}

