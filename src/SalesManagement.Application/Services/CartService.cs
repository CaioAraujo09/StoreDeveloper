using SalesManagement.Application.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesManagement.Application.Services;
public class CartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IRegisteredUserRepository _userRepository;
    public CartService(ICartRepository cartRepository, IRegisteredUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
    }

    public async Task<Cart?> GetCartByIdAsync(Guid cartId)
    {
        return await _cartRepository.GetCartByIdAsync(cartId);
    }

    public async Task<IEnumerable<Cart>> GetAllCartsAsync()
    {
        return await _cartRepository.GetAllCartsAsync();
    }

    public async Task AddCartAsync(CartDto cartDto)
    {
        try
        {
            var registeredUser = await _userRepository.GetByIdAsync(cartDto.UserId);
            if (registeredUser == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var cartItems = cartDto.Products.Select(dto => new CartItem(dto.ProductId, dto.Quantity)).ToList();

            var cart = new Cart(registeredUser, cartDto.Date, cartItems);

            await _cartRepository.AddCartAsync(cart);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar no banco: {ex.InnerException?.Message ?? ex.Message}");
            throw;
        }
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        await _cartRepository.UpdateCartAsync(cart);
    }

    public async Task DeleteCartAsync(Guid cartId)
    {
        await _cartRepository.DeleteCartAsync(cartId);
    }
}
