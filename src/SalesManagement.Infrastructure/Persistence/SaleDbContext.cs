using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrasctructure.Persistence
{
    public class SaleDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SaleDbContext(DbContextOptions<SaleDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("PostgresConnection");

                optionsBuilder.UseNpgsql(connectionString,
                    x => x.MigrationsAssembly("SalesManagement.Infrastructure"));
            }
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; } 

    }
}
