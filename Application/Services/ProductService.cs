using shop_api.Common;
using shop_api.Domain.Entities;
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

    public async Task<Result<List<Product>, Exception>> GetProductsAsync()
    {
        return await _productRepository.GetProductsAsync();
    }

    public async Task<Result<Product?, Exception>> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }
    
    public async Task<Result<Product, Exception>> AddProductAsync(ProductViewModel incoming)
    {
        return await _productRepository.AddProductAsync(incoming.ToProduct());
    }
    
    public async Task<Result<Product, Exception>> UpdateProductAsync(int id, ProductViewModel updated)
    {
        return await _productRepository.UpdateProductAsync(id, updated.ToProduct());
    }
    
    public async Task<Result<Product, Exception>> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteProductAsync(id);
    }
}