using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using SalesManagement.Infrasctructure.Persistence;
using SalesManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesManagement.Infrastructure.Persistence.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly SaleDbContext _dbContext;

        public BranchRepository(SaleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _dbContext.Branchs.ToListAsync();
        }

        public async Task<Branch?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Branchs.FindAsync(id);
        }

        public async Task AddAsync(Branch branch)
        {
            _dbContext.Branchs.Add(branch);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Branch branch)
        {
            _dbContext.Branchs.Update(branch);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var branch = await _dbContext.Branchs.FindAsync(id);
            if (branch != null)
            {
                _dbContext.Branchs.Remove(branch);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
