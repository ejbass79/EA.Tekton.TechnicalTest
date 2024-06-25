using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;

public interface IRepositoryFactory
{
    IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
}
