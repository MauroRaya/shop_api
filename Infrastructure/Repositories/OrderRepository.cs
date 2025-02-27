using Microsoft.EntityFrameworkCore;
using shop_api.Domain.Entities;
using shop_api.Infrastructure.Contexts;

namespace shop_api.Infrastructure.Repositories;

public class OrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Products)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
    
    public async Task AddOrderAsync(Order incoming)
    {
        await _context.Orders.AddAsync(incoming);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var found = await _context.Orders.FindAsync(id);
        
        if (found is null)
            return;
        
        _context.Orders.Remove(found);
        await _context.SaveChangesAsync();
    }
}