using EA.Tekton.TechnicalTest.Cross.Dto;
using System.Linq.Expressions;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> AsQueryable();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<QueryMultipleResponse<TEntity>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true);

    Task<QueryMultipleResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, int page, int limit, string orderBy, bool ascending = true);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

    void Insert(TEntity entity);

    void Insert(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Update(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void Delete(object id);

    void Delete(IEnumerable<TEntity> entities);
}
