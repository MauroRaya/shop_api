using shop_api.Shared;
using shop_api.Domain.Entities;
using shop_api.Infra.UOW;

namespace shop_api.Application.Services;

public class ProductService
{
    private readonly UnitOfWork _uow;

    public ProductService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<List<Product>, Exception>> GetProductsAsync()
    {
        try
        {
            var productRepository = _uow.GetRepository<Product>();
            return await productRepository.GetAsync();
        }
        catch (Exception ex)
        {
            return ex;
        }
        finally
        {
            _uow.Dispose();
        }
    }

    public async Task<Result<Product, Exception>> GetProductByIdAsync(int id)
    {
        try
        {
            var productRepository = _uow.GetRepository<Product>();
            var product = await productRepository.GetByIdAsync(id);
            
            if (product is null)
                return new Exception("Product not found");   
            
            return product;
        }
        catch (Exception ex)
        {
            return ex;
        }
        finally
        {
            _uow.Dispose();
        }
    }
    
    public async Task<Result<Product, Exception>> AddProductAsync(Product product)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return new Exception("Product data is required");
        
            if (product.Price <= 0)
                return new Exception("Product price cannot be less or equal to 0");
        
            var productRepository = _uow.GetRepository<Product>();
            await productRepository.AddAsync(product);
            await _uow.SaveChangesAsync();
            
            return product;
        }
        catch (Exception ex)
        {
            return ex;
        }
        finally
        {
            _uow.Dispose();
        }
    }
    
    public async Task<Result<Product, Exception>> UpdateProductAsync(Product product)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return new Exception("Product data is required");
        
            if (product.Price <= 0)
                return new Exception("Product price cannot be less or equal to 0");
        
            var productRepository = _uow.GetRepository<Product>();
            productRepository.Update(product);
            await _uow.SaveChangesAsync();

            return product;
        }
        catch (Exception ex)
        {
            return ex;
        }
        finally
        {
            _uow.Dispose();
        }
    }
    
    public async Task<Result<Product, Exception>> DeleteProductAsync(Product product)
    {
        try
        {
            var productRepository = _uow.GetRepository<Product>();
            productRepository.RemoveAsync(product);
            await _uow.SaveChangesAsync();
            
            return product;
        }
        catch (Exception ex)
        {
            return ex;
        }
        finally
        {
            _uow.Dispose();
        }
    }
}