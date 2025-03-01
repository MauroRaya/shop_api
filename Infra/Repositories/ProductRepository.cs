using Microsoft.EntityFrameworkCore;
using shop_api.Common;
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
        => await _context.Products.ToListAsync();
    
    public async Task<Product?> GetProductByIdAsync(int id) 
        => await _context.Products.FindAsync(id);
    
    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}