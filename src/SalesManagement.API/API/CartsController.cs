using Microsoft.AspNetCore.Mvc;
using SalesManagement.API.Helpers;
using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;  
using SalesManagement.Domain.Entities;

namespace SalesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartsController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCartById(Guid id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            return cart == null ? NotFound() : Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartDto cartDto)
        {
            if (cartDto == null)
                return BadRequest("Dados inválidos.");

            if (cartDto.RegisteredUserId == Guid.Empty)
                return BadRequest("O campo 'UserId' é obrigatório.");

            if (cartDto.Products == null || cartDto.Products.Count == 0)
                return BadRequest("O carrinho deve conter pelo menos um produto.");

            try
            {
                await _cartService.AddCartAsync(cartDto);
                return CreatedAtAction(nameof(GetCartById), new { id = cartDto.Id }, cartDto);
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex, "Erro ao criar o carrinho.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            await _cartService.DeleteCartAsync(id);
            return NoContent();
        }
    }
}
