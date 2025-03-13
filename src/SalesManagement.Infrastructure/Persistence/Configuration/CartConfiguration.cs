using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Persistence;

namespace SalesManagement.Infrasctructure.Persistence.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Date)
            .IsRequired()
                .HasColumnType("date");

            builder.HasOne(c => c.RegisteredUser)
                .WithMany()
                .HasForeignKey(c => c.RegisteredUserId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(c => c.Products)
                .WithOne()
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
