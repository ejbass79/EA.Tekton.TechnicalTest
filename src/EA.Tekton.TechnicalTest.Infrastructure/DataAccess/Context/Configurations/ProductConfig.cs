using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Constants;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Context.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> modelBuilder)
    {
        modelBuilder.ToTable("Product", SchemaNames.Core);

        modelBuilder.HasKey(e => e.ProductId).HasName("PK_Products");

        modelBuilder.Property(e => e.ProductId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        modelBuilder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        modelBuilder.Property(e => e.StatusId)
            .IsRequired();

        modelBuilder.Property(e => e.Stock)
            .IsRequired();

        modelBuilder.Property(e => e.Description)
            .HasMaxLength(250)
            .IsUnicode(false);

        modelBuilder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");

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

        modelBuilder.HasIndex(e => e.CreationDate, name: "IX_Product_CreationDate");

        modelBuilder.HasIndex(e => e.StatusId, "IX_Product_StatusId");

        modelBuilder.HasOne<State>().WithMany().HasForeignKey(r => r.StatusId);
    }
}