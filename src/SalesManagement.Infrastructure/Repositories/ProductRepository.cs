using Microsoft.EntityFrameworkCore;
using SalesManagement.Application.Common;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using SalesManagement.Infrasctructure.Persistence;

namespace SalesManagement.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SaleDbContext _context;

        public ProductRepository(SaleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int page, int size, string order, Dictionary<string, string> filters)
        {
            var query = _context.Products
                .Include(p => p.Rating)
                .AsQueryable();

            query = query.ApplyFilters(filters);
            query = query.ApplySorting(order);
            query = query.ApplyPagination(page, size);

            return await query.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Rating) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page, int size, string order)
        {
            return await _context.Products
                .Include(p => p.Rating)
                .Where(p => p.Category == category)
                .OrderBy(p => p.Title)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task AddAsync(Product product, Rating rating)
        {
            await _context.Products.AddAsync(product);
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product, Rating rating)
        {
            _context.Products.Update(product);
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.ProductId == id);
                if (rating != null)
                {
                    _context.Ratings.Remove(rating); 
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
