using Microsoft.EntityFrameworkCore;
using shop_api.Infra.Contexts;
using shop_api.Infra.Repositories;

namespace shop_api.Infra.UOW;

public class UnitOfWork : IDisposable
{
    private readonly AppDbContext _context;
    private bool _disposed = false;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Repository<T> GetRepository<T>() where T : class
    {
        return new Repository<T>(_context);
    }

    public void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class
    {
        _context.Entry(entity).State = state;
    }

    public void Attach<T>(T entity) where T : class
    {
        _context.Attach(entity);
    }

    public void Detach<T>(T entity) where T : class
    {
        var entry = _context.Entry(entity);

        if (entry.State is not EntityState.Detached)
        {
            entry.State = EntityState.Detached;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}