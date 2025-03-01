using shop_api.Common;
using shop_api.Domain.ViewModels;
using shop_api.Infra.Repositories;

namespace shop_api.Application.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ICustomResult> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        
        return products.Count.Equals(0)
            ? ResponseFactory.Failure(404, "Products is empty")
            : ResponseFactory.Success(products);
    }

    public async Task<ICustomResult> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        
        return product is null
            ? ResponseFactory.Failure(404, "Products not found")
            : ResponseFactory.Success(product);
    }
    
    public async Task<ICustomResult> AddProductAsync(ProductViewModel incoming)
    {
        await _productRepository.AddProductAsync(incoming.ToProduct());
        return ResponseFactory.Success(incoming);
    }
    
    public async Task<ICustomResult> UpdateProductAsync(int id, ProductViewModel updated)
    {
        var old = await _productRepository.GetProductByIdAsync(id);
        
        if (old is null)
            return ResponseFactory.Failure(404, "Product not found");
        
        await _productRepository.UpdateProductAsync(old, updated);
        return ResponseFactory.Success(updated);
    }
    
    public async Task<ICustomResult> DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        
        if (product is null)
            return ResponseFactory.Failure(404, "Product not found");
        
        await _productRepository.DeleteProductAsync(product);
        return ResponseFactory.Success(product);
    }
}