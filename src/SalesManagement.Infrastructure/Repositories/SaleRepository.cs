using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Infrasctructure.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SaleDbContext _dbContext;

        public SaleRepository(SaleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sale?> GetByIdAsync(Guid saleId)
        {
            return await _dbContext.Sales
               .Include(s => s.Items)
               .ThenInclude(si => si.Product)
               .Include(s => s.RegisteredUser)
               .FirstOrDefaultAsync(s => s.Id == saleId);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _dbContext.Sales
                .Include(s => s.Items)
                .ThenInclude(si => si.Product)
               .Include(s => s.RegisteredUser)
                .ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _dbContext.Sales.Update(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid saleId)
        {
            var sale = await _dbContext.Sales.FindAsync(saleId);
            if (sale != null)
            {
                _dbContext.Sales.Remove(sale);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Sale?> GetBySaleNumberAsync(string saleNumber)
        {
            return await _dbContext.Sales.FirstOrDefaultAsync(s => s.SaleNumber == saleNumber);
        }
    }
}
