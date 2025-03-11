using Microsoft.EntityFrameworkCore;
using shop_api.Infra.Contexts;

namespace shop_api.Infra.Repositories;

public class Repository<T> where T : class
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);  
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void RemoveAsync(T entity)
    {
        _context.Remove(entity);
    }
}