using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using SalesManagement.Application.DTOs;

namespace SalesManagement.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int page, int size, string order, Dictionary<string, string> filters)
        {
            return await _productRepository.GetAllAsync(page, size, order, filters);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, int page, int size, string order)
        {
            return await _productRepository.GetByCategoryAsync(category, page, size, order);
        }

        public async Task<IEnumerable<string>> GetAllCategoriesAsync()
        {
            return await _productRepository.GetAllCategoriesAsync();
        }

        public async Task AddAsync(ProductDto dto)
        {
            var product = new Product(dto.Title, dto.Price, dto.Description, dto.Category, dto.Image);
            var rating = new Rating(product.Id, dto.Rating.Rate, dto.Rating.Count);

            await _productRepository.AddAsync(product, rating);
        }

        public async Task UpdateAsync(Guid id, ProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.Update(dto.Title, dto.Price, dto.Description, dto.Category, dto.Image);

                var rating = new Rating(product.Id, dto.Rating.Rate, dto.Rating.Count);

                await _productRepository.UpdateAsync(product, rating);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
