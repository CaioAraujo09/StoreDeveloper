using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;

namespace SalesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
         [FromQuery] int _page = 1,
         [FromQuery] int _size = 10,
         [FromQuery] string _order = "title asc")
        {
            var filters = Request.Query
                .Where(q => q.Key != "_page" && q.Key != "_size" && q.Key != "_order")
                .ToDictionary(q => q.Key, q => q.Value.ToString());

            var products = await _productService.GetAllAsync(_page, _size, _order, filters);

            return Ok(new
            {
                data = products,
                totalItems = products.Count(),
                currentPage = _page,
                totalPages = (int)Math.Ceiling((double)products.Count() / _size)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound("Produto não encontrado.") : Ok(product);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category, [FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "title asc")
        {
            var products = await _productService.GetByCategoryAsync(category, _page, _size, _order);
            return Ok(products);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            return Ok(categories);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Dados inválidos.");

            await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Title }, productDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Dados inválidos.");

            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound("Produto não encontrado.");

            await _productService.UpdateAsync(id, productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound("Produto não encontrado.");

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}