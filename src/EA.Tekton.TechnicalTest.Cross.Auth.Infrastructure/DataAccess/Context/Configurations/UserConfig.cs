using EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Constants;
using EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Context.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.ToTable(TableNames.User, SchemaNames.Auth);

        modelBuilder.HasIndex(e => e.Email, "IX_Users_Email");
    }
}