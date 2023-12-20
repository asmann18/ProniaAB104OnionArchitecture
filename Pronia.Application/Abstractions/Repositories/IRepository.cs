using System.Linq.Expressions;

namespace Pronia.Application.Abstractions.Repositories;

public interface IRepository<T> where T : class, new()
{
    IQueryable<T> GetAllAsync(bool isTracking = false, params string[] includes);
    IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expression);
    IQueryable<T> Paginate(IQueryable<T> query, int limit, int page = 1);
    IQueryable<T> GetFilteredAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveAsync();
}
