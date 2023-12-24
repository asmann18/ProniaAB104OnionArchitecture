using Microsoft.EntityFrameworkCore;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Domain.Entities.Common;
using Pronia.Persistence.Contexts;
using System.Linq.Expressions;

namespace Pronia.Persistence.Implementations.Repositories;

public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        if (entity is BaseAuditableEntity auditableEntity)
        {
            auditableEntity.IsDeleted = true;
        }
        else
        {
            _context.Set<T>().Remove(entity);
        }
    }

    public IQueryable<T> GetAll( params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;

        //if (!isTracking)
        //{
        //    entities = entities.AsNoTracking();
        //}

    }



    public IQueryable<T> GetFilteredAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().Where(expression).AsQueryable();
        foreach (string include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().AnyAsync(expression);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable<T>();
        foreach (var include in includes)
        {
            query.Include(include);
        }
        T? entity = await query.FirstOrDefaultAsync(expression);
        return entity;
    }

    public IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expression)
    {
        IQueryable<T> result = query.OrderBy(expression);
        return result;
    }

    public IQueryable<T> Paginate(IQueryable<T> query, int limit, int page = 1)
    {
        IQueryable<T> result = query.Skip((page - 1) * limit).Take(limit);
        return result;
    }

}
