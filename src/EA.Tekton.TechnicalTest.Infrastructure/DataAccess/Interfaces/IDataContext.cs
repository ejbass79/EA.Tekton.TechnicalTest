using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;

public interface IDataContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveAsync();

    int Save();

    void Clear();

    void RollBack();
}
