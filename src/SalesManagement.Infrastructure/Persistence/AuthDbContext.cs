using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration)
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

        public DbSet<User> Users { get; set; }
    }
}
