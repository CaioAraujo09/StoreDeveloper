using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Infrasctructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SaleDbContext _dbContext;

        public CartRepository(SaleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cart?> GetCartByIdAsync(Guid cartId)
        {
            return await _dbContext.Carts
                 .AsNoTracking()  
                .Include(c => c.RegisteredUser)
                .Include(c => c.Products)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _dbContext.Carts
                .Include(c => c.RegisteredUser)
                .Include(c => c.Products)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _dbContext.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _dbContext.Carts.Remove(cart);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}