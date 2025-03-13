using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int page, int size, string order, Dictionary<string, string> filters);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page, int size, string order);
        Task<IEnumerable<string>> GetAllCategoriesAsync();
        Task AddAsync(Product product, Rating rating);
        Task UpdateAsync(Product product, Rating rating);
        Task DeleteAsync(Guid id);
    }
}
