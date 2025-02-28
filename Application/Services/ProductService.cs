using shop_api.Infra.Repositories;

namespace shop_api.Application.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void GetProducts()
    {
        //fazer
    }
}