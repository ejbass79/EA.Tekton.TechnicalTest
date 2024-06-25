using EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Entities;
using EA.Tekton.TechnicalTest.Cross.Configuration;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

using SchemaNames = EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Constants.SchemaNames;
using TableNames = EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Constants.TableNames;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Context;

public sealed class EATektonTechnicalTestContext : IdentityDbContext<User>, IDataContext
{
    public EATektonTechnicalTestContext()
    { }

    public EATektonTechnicalTestContext(DbContextOptions<EATektonTechnicalTestContext> dbContextOptions) : base(dbContextOptions)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public int Save()
    {
        try
        {
            var count = SaveChanges();
            return count;
        }
        catch
        {
            Clear();
            throw;
        }
    }

    public async Task<int> SaveAsync()
    {
        try
        {
            var count = await SaveChangesAsync();
            return count;
        }
        catch
        {
            Clear();
            throw;
        }
    }

    public void Clear()
    {
        var clearables = ChangeTracker.Entries().Where(e =>
            e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();

        clearables.ForEach(x => x.State = EntityState.Detached);
    }

    public void RollBack()
    {
        ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaNames.Auth);
        modelBuilder.HasDefaultSchema(Constants.SchemaNames.Core);

        modelBuilder.CascadeAllRelationsOnDelete();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable(TableNames.User, SchemaNames.Auth);
        modelBuilder.Entity<IdentityRole>().ToTable(TableNames.IdentityRole, SchemaNames.Auth);
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable(TableNames.IdentityUserRole, SchemaNames.Auth);
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable(TableNames.IdentityRoleClaim, SchemaNames.Auth);
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable(TableNames.IdentityUserClaim, SchemaNames.Auth);
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable(TableNames.IdentityUserLogin, SchemaNames.Auth);
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable(TableNames.IdentityUserToken, SchemaNames.Auth);

        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        var roleUser = new
        {
            Id = Guid.NewGuid().ToString(),
            Name = CrossConfiguration.DefaultRolUser,
            NormalizedName = CrossConfiguration.DefaultRolUser
        };

        var states = new List<State>
        {
            new ()
            {
                StatusId = 0,
                StatusName   = "Inactive",
                CreationDate = DateTime.Now,
                CreationUser = "EDWIN AVILA",
                Deleted = false
            },
            new ()
            {
                StatusId = 1,
                StatusName   = "Active",
                CreationDate = DateTime.Now,
                CreationUser = "EDWIN AVILA",
                Deleted = false
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roleUser);
        modelBuilder.Entity<State>().HasData(states);
    }
}