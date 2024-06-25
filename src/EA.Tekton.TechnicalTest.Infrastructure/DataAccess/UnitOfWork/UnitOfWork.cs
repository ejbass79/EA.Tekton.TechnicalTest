using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;

using Microsoft.AspNetCore.Identity;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDataContext _dataContext;
    private readonly IRepositoryFactory _repositoryFactory;

    public UnitOfWork(IDataContext dataContext, IRepositoryFactory repositoryFactory)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    public UnitOfWork()
    { }

    public void Dispose() => _dataContext.Dispose();

    public bool Save() => _dataContext.Save() > 0;

    public async Task<bool> SaveAsync()
    {
        return await _dataContext.SaveAsync() > 0;
    }

    public void Clear() => _dataContext.Clear();

    public void Rollback() => _dataContext.RollBack();

    public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
    {
        return _repositoryFactory.CreateRepository<TEntity>();
    }
}
