using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IDataContext _dataContext;

    public RepositoryFactory(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity>(_dataContext);
    }
}
