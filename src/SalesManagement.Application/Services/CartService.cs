using Microsoft.EntityFrameworkCore;
using SalesManagement.Application.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesManagement.Application.Services;
public class CartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IRegisteredUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IRegisteredUserRepository userRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
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
            var registeredUser = await _userRepository.GetByIdAsync(cartDto.RegisteredUserId);
            if (registeredUser == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var cartItems = new List<CartItem>();

            foreach (var dto in cartDto.Products)
            {
                var product = await _productRepository.GetByIdAsync(dto.ProductId);
                if (product == null)
                {
                    throw new Exception($"Produto com ID {dto.ProductId} não encontrado.");
                }

                var cartItem = CartItem.Create(dto.ProductId, dto.Quantity, product.Price);
                cartItems.Add(cartItem);
            }
            var cart = new Cart(registeredUser, cartDto.Date, cartItems);
            await _cartRepository.AddCartAsync(cart);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar no banco: {ex.InnerException?.Message ?? ex.Message}");
            throw;
        }
    }

    public async Task DeleteCartAsync(Guid cartId)
    {
        await _cartRepository.DeleteCartAsync(cartId);
    }
}
