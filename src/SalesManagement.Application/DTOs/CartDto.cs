namespace SalesManagement.Application.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }

        private List<CartItemDto> _products = new();
        public List<CartItemDto> Products
        {
            get => _products ??= new();
            set => _products = value ?? new();
        }
    }
}