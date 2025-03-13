using System.Text.Json.Serialization;

namespace SalesManagement.Domain.Entities;

public class Cart
{
    public Guid Id { get; private set; }
    public Guid RegisteredUserId { get; private set; }
    public DateTime Date { get; private set; }
    public List<CartItem> Products { get; private set; } = new();

    public RegisteredUser RegisteredUser { get; private set; } 

    private Cart() { }

    public Cart(RegisteredUser registeredUser, DateTime date, List<CartItem> products)
    {
        Id = Guid.NewGuid();
        RegisteredUser = registeredUser ?? throw new ArgumentNullException(nameof(registeredUser));
        RegisteredUserId = registeredUser.Id;
        Date = date;
        Products = products ?? new List<CartItem>();
    }
}