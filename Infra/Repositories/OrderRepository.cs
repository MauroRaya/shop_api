using Microsoft.EntityFrameworkCore;
using shop_api.Domain.Entities;
using shop_api.Infra.Contexts;

namespace shop_api.Infra.Repositories;

public class OrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersAsync() 
        => await _context.Orders.Include(o => o.Products).ToListAsync();
    
    public async Task<Order?> GetOrderByIdAsync(int id) 
        => await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);
    
    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteOrderAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}