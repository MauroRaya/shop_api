using Microsoft.EntityFrameworkCore;
using shop_api.Domain.Entities;
using shop_api.Infrastructure.Contexts;

namespace shop_api.Infrastructure.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddProduct(Product incoming)
    {
        await _context.Products.AddAsync(incoming);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(int id, Product incoming)
    {
        var found = await _context.Products.FindAsync(id);

        if (found is null)
            return;
        
        _context.Entry(found).CurrentValues.SetValues(incoming);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var found = await _context.Products.FindAsync(id);
        
        if (found is null)
            return;
        
        _context.Products.Remove(found);
        await _context.SaveChangesAsync();
    }
}