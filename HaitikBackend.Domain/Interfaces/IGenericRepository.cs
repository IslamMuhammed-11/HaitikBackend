namespace HaitikBackend.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetById(int Id, CancellationToken ct);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    IQueryable<T> Query();

    Task<int> SaveChangesAsync(CancellationToken ct);
}
