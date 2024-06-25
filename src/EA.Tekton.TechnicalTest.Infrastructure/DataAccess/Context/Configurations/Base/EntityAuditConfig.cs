using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Context.Configurations.Base;

public class EntityAuditConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityAudit
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreationDate)
            .HasColumnName("CreationDate")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(e => e.CreationUser)
            .HasColumnName("CreationUser")
            .HasMaxLength(300)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(e => e.ModificationDate)
            .HasColumnName("ModificationDate")
            .HasColumnType("datetime");

        builder.Property(e => e.ModificationUser)
            .HasColumnName("ModificationUser")
            .HasMaxLength(300)
            .IsUnicode(false);

        builder.Property(e => e.Deleted)
            .HasColumnName("Deleted")
            .IsUnicode(false);
    }
}
