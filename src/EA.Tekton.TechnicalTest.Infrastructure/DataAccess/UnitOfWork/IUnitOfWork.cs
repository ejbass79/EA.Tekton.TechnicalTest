using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    new void Dispose();

    bool Save();

    Task<bool> SaveAsync();

    void Rollback();

    IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
}
