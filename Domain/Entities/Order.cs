namespace shop_api.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}