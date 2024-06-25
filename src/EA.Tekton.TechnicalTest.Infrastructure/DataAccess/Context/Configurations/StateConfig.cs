using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Constants;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Context.Configurations;

public class StateConfig : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> modelBuilder)
    {
        modelBuilder.ToTable("State", SchemaNames.Core);

        modelBuilder.HasKey(e => e.StatusId).HasName("PK_States");

        modelBuilder.Property(e => e.StatusId)
            .ValueGeneratedNever()
            .IsRequired();

        modelBuilder.Property(e => e.StatusName)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        modelBuilder.Property(e => e.CreationDate)
            .HasColumnType("datetime")
            .HasDefaultValue(DateTime.Now)
            .IsRequired();

        modelBuilder.Property(e => e.CreationUser)
            .HasMaxLength(300)
            .IsUnicode(false)
            .IsRequired();

        modelBuilder.Property(e => e.ModificationDate)
            .HasColumnType("datetime");

        modelBuilder.Property(e => e.ModificationUser)
            .HasMaxLength(300)
            .IsUnicode(false);

        modelBuilder.Property(e => e.Deleted)
            .IsUnicode(false)
            .HasDefaultValue(false);

        modelBuilder.HasIndex(e => e.CreationDate, name: "IX_State_CreationDate");
    }
}