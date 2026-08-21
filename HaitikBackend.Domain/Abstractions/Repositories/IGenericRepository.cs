namespace HaitikBackend.Domain.Abstractions.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int Id, CancellationToken ct);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    IQueryable<T> Query();

}
