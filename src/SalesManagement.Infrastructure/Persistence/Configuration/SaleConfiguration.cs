using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrasctructure.Persistence.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.BranchId).IsRequired().HasMaxLength(50);
            builder.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");

            builder.Property(s => s.SaleNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(s => s.Items)
                   .WithOne(i => i.Sale)
                   .HasForeignKey(i => i.SaleId);

            builder.HasOne(s => s.RegisteredUser)
                .WithMany()
                .HasForeignKey(s => s.RegisteredUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Branch)
                   .WithMany()
                   .HasForeignKey(s => s.BranchId)
                   .OnDelete(DeleteBehavior.Restrict) 
                   .HasConstraintName("FK_Sales_Branch");
        }
    }
}