using Microsoft.EntityFrameworkCore;
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

            if (!string.IsNullOrEmpty(order))
            {
                var orderParams = order.Split(',');
                foreach (var param in orderParams)
                {
                    var trimmed = param.Trim();
                    if (trimmed.EndsWith("desc"))
                    {
                        query = query.OrderByDescending(x => EF.Property<object>(x, trimmed.Replace("desc", "").Trim()));
                    }
                    else
                    {
                        query = query.OrderBy(x => EF.Property<object>(x, trimmed.Replace("asc", "").Trim()));
                    }
                }
            }

            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
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
