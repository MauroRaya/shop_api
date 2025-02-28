using Microsoft.EntityFrameworkCore;
using shop_api.Domain.Entities;
using shop_api.Infra.Contexts;

namespace shop_api.Infra.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddProductAsync(Product incoming)
    {
        await _context.Products.AddAsync(incoming);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(int id, Product incoming)
    {
        var found = await _context.Products.FindAsync(id);

        if (found is null)
            return;
        
        _context.Entry(found).CurrentValues.SetValues(incoming);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var found = await _context.Products.FindAsync(id);
        
        if (found is null)
            return;
        
        _context.Products.Remove(found);
        await _context.SaveChangesAsync();
    }
}