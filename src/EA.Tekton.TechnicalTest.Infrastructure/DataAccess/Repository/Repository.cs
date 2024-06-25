using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IDataContext _dataContext;

    private readonly DbSet<TEntity> _entities;

    public Repository(IDataContext dataContext)
    {
        _dataContext = dataContext;
        _entities = dataContext.Set<TEntity>();
    }

    private IQueryable<TEntity> PerformInclusions(IEnumerable<Expression<Func<TEntity, object>>> includeProperties, IQueryable<TEntity> query)
    {
        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }

#pragma warning disable 1998

    public async Task<IQueryable<TEntity>> AsQueryable()
    {
        return _entities.AsQueryable();
    }

#pragma warning disable 1998

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = await AsQueryable();
        query = PerformInclusions(includeProperties, query);
        return query.Where(filter);
    }

    public async Task<QueryMultipleResponse<TEntity>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
    {
        var result = await PagedResult<TEntity>.ToPagedListAsync(_entities.AsQueryable(), page, limit, orderBy, ascending);
        var pagedResult = new QueryMultipleResponse<TEntity>
        {
            CurrentPage = result.CurrentPage,
            TotalCount = result.TotalCount,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Result = result
        };
        return pagedResult;
    }

    public async Task<QueryMultipleResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, int page, int limit, string orderBy, bool ascending = true)
    {
        var result = await PagedResult<TEntity>.ToPagedListAsync(_entities.AsQueryable().Where(filter), page, limit, orderBy, ascending);

        var pagedResult = new QueryMultipleResponse<TEntity>
        {
            CurrentPage = result.CurrentPage,
            TotalCount = result.TotalCount,
            PageSize = result.PageSize,
            TotalPages = result.TotalPages,
            Result = result
        };

        return pagedResult;
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = await AsQueryable();
        query = PerformInclusions(includeProperties, query);
        return query.FirstOrDefault(filter);
    }

    public async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = await AsQueryable();
        query = PerformInclusions(includeProperties, query);
        return query.LastOrDefault(filter);
    }

    public virtual void Insert(TEntity entity)
    {
        _entities.Add(entity);
    }

    public void Insert(IEnumerable<TEntity> entities)
    {
        foreach (var e in entities)
        {
            _dataContext.Entry(e).State = EntityState.Added;
        }
    }

    public void Update(TEntity entity)
    {
        _entities.Attach(entity);
        _dataContext.Entry(entity).State = EntityState.Modified;
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        foreach (var e in entities)
        {
            _dataContext.Entry(e).State = EntityState.Modified;
        }
    }

    public void Delete(TEntity entity)
    {
        if (_dataContext.Entry(entity).State == EntityState.Detached)
        {
            _entities.Attach(entity);
        }
        _entities.Remove(entity);
    }

    public void Delete(object id)
    {
        TEntity entityToDelete = _entities.Find(id);
        _entities.Remove(entityToDelete);
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        foreach (var e in entities)
        {
            _dataContext.Entry(e).State = EntityState.Deleted;
        }
    }
}
