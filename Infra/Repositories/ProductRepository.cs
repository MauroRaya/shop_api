using Microsoft.EntityFrameworkCore;
using shop_api.Common;
using shop_api.Domain.Entities;
using shop_api.Domain.ViewModels;
using shop_api.Infra.Contexts;

namespace shop_api.Infra.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Product>, Exception>> GetProductsAsync()
    {
        try
        {
            return await _context.Products.ToListAsync();
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Result<Product?, Exception>> GetProductByIdAsync(int id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return new Exception("Product not found");    
            }
            
            return product;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Result<Product, Exception>> AddProductAsync(Product product)
    {
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    
    public async Task<Result<Product, Exception>> UpdateProductAsync(int id, Product updatedProduct)
    {
        try
        {
            var productDb = await _context.Products.FindAsync(id);
            if (productDb is null)
            {
                return new Exception("Product not found");    
            }
            
            _context.Entry(productDb).CurrentValues.SetValues(updatedProduct);
            await _context.SaveChangesAsync();

            return updatedProduct;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    
    public async Task<Result<Product, Exception>> DeleteProductAsync(int id)
    {
        try
        {
            var productDb = await _context.Products.FindAsync(id);
            if (productDb is null)
            {
                return new Exception("Product not found");    
            }
            
            _context.Products.Remove(productDb);
            await _context.SaveChangesAsync();
            
            return productDb;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}