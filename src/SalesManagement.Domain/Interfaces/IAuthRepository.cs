using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(Guid userId);
        Task AddAsync(User user);
        void Attach(User user);
    }
}