namespace shop_api.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Product()
    {
        
    }

    public Product(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
    }
    
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}