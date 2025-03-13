using Microsoft.EntityFrameworkCore;
using SalesManagement.Application.Common;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using SalesManagement.Infrasctructure.Persistence;

namespace SalesManagement.Infrastructure.Persistence.Repositories
{
    public class RegisteredUserRepository : IRegisteredUserRepository
    {
        private readonly SaleDbContext _context;

        public RegisteredUserRepository(SaleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegisteredUser>> GetAllAsync(int page, int size, string order)
        {
            var query = _context.RegisteredUsers.AsQueryable();
            query = query.ApplySorting(order);
            query = query.ApplyPagination(page, size);

            return await query.ToListAsync();
        }

        public async Task<RegisteredUser?> GetByIdAsync(Guid id)
        {
            return await _context.RegisteredUsers.FindAsync(id);
        }

        public async Task AddAsync(RegisteredUser user)
        {
            await _context.RegisteredUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RegisteredUser user)
        {
            _context.RegisteredUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.RegisteredUsers.FindAsync(id);
            if (user != null)
            {
                _context.RegisteredUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
