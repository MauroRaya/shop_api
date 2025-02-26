using Microsoft.EntityFrameworkCore;
using shop_api.Domain.Entities;

namespace shop_api.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}