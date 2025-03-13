namespace SalesManagement.Domain.Entities;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; } 

    public Product Product { get; private set; }
    public Cart Cart { get; private set; }

    private CartItem() { }

    private CartItem(Guid productId, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static CartItem Create(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade do produto deve ser maior que zero.");

        if (unitPrice <= 0)
            throw new ArgumentException("O preço unitário do produto deve ser maior que zero.");

        return new CartItem(productId, quantity, unitPrice);
    }
}