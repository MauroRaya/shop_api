using shop_api.Domain.Entities;

namespace shop_api.Domain.ViewModels;

public class ProductViewModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Product ToProduct()
    {
        return new Product(Name, Price);
    }
}