using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Persistence.Configuration
{
    public class RegisteredUserConfiguration : IEntityTypeConfiguration<RegisteredUser>
    {
        public void Configure(EntityTypeBuilder<RegisteredUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.City).HasMaxLength(100);
            builder.Property(u => u.Street).HasMaxLength(200);
            builder.Property(u => u.Zipcode).HasMaxLength(20);
            builder.Property(u => u.Latitude).HasMaxLength(50);
            builder.Property(u => u.Longitude).HasMaxLength(50);
            builder.Property(u => u.Phone).HasMaxLength(20);

            builder.Property(u => u.Status)
               .HasConversion<string>()
               .IsRequired();

            builder.Property(u => u.Role)
                .HasConversion<string>() 
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique(); 
            builder.HasIndex(u => u.Username).IsUnique(); 

            builder.ToTable("RegisteredUsers");
        }
    }
}
