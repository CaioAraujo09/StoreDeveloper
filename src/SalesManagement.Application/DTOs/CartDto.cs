using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Application.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo 'UserId' é obrigatório.")]
        public Guid RegisteredUserId { get; set; }

        [Required(ErrorMessage = "O campo 'Username' é obrigatório.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A data do carrinho é obrigatória.")]
        public DateTime Date { get; set; }

        private List<CartItemDto> _products = new();

        [Required(ErrorMessage = "O carrinho deve conter pelo menos um produto.")]
        [MinLength(1, ErrorMessage = "O carrinho deve conter pelo menos um produto.")]
        public List<CartItemDto> Products
        {
            get => _products ??= new();
            set => _products = value ?? new();
        }
    }
}
