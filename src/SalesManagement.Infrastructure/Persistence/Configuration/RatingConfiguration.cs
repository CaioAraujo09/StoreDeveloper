using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Persistence.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rate)
                .HasColumnType("decimal(3,2)")
                .IsRequired();

            builder.Property(r => r.Count)
                .IsRequired();

            builder.ToTable("Ratings");
        }
    }
}
