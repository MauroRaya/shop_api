using Microsoft.EntityFrameworkCore;
using shop_api.Domain.Entities;
using shop_api.Infra.Contexts;

namespace shop_api.Infra.Repositories;

public class RepositoryException : Exception
{
    public RepositoryException(string message, Exception innerException) : base(message, innerException) {}
}

public class OrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        try
        {
            return await _context.Orders
                .Include(o => o.Products)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error fetching orders", ex);
        }

    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        try
        {
            return await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error fetching order with ID {id}", ex);
        }
    }
    
    public async Task AddOrderAsync(Order incoming)
    {
        try
        {
            await _context.Orders.AddAsync(incoming);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error adding new order", ex);
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        try
        {
            var found = await _context.Orders.FindAsync(id);
        
            if (found is null)
                return;
        
            _context.Orders.Remove(found);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Error deleting order with ID {id}", ex);
        }
    }
}