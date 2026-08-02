using HaitikBackend.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly HaitikDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public GenericRepository(HaitikDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual async Task<T?> GetByIdAsync(int Id, CancellationToken ct)
    {
        var result = await _dbSet.FindAsync(new object[] { Id }, ct);
        return result is null ? null : (T)result;
    }
}
