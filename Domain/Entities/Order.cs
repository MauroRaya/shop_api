namespace shop_api.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
}