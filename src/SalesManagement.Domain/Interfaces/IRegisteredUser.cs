using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Interfaces
{
    public interface IRegisteredUserRepository
    {
        Task<IEnumerable<RegisteredUser>> GetAllAsync(int page, int size, string order);
        Task<RegisteredUser?> GetByIdAsync(Guid id);
        Task AddAsync(RegisteredUser user);
        Task UpdateAsync(RegisteredUser user);
        Task DeleteAsync(Guid id);
    }
}
