using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByIdAsync(Guid cartId);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task AddCartAsync(Cart cart);
        Task DeleteCartAsync(Guid cartId);
    }
}