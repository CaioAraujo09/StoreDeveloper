using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid saleId);
        Task<IEnumerable<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(Guid saleId);
        Task<Sale?> GetBySaleNumberAsync(string saleNumber);

    }
}