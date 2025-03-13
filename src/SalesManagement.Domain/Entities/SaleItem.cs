using System.Text.Json.Serialization;

namespace SalesManagement.Domain.Entities;

[JsonSerializable(typeof(SaleItem))]
public class SaleItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity - Discount;
    public Guid SaleId { get; set; }

    [JsonIgnore]
    public Sale? Sale { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }

    private SaleItem() { }

    public SaleItem(Guid productId, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = 0;
    }
    public static SaleItem Create(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade do item deve ser maior que zero.");

        if (unitPrice <= 0)
            throw new ArgumentException("O preço unitário do item deve ser maior que zero.");

        (decimal discount, string message) = CalculateDiscount(quantity, unitPrice);
        Console.WriteLine(message); 

        return new SaleItem(productId, quantity, unitPrice)
        {
            Discount = discount
        };

    }

    private static (decimal discount, string message) CalculateDiscount(int quantity, decimal unitPrice)
    {
        decimal discount = 0m;
        string message;

        if (quantity > 20)
            throw new InvalidOperationException("Não é possível vender acima de 20 itens idênticos.");

        if (quantity >= 10)
        {
            discount = unitPrice * quantity * 0.2m;
            message = $"✅ Desconto aplicado: 20% - Total com desconto: {unitPrice * quantity - discount:C}";
        }
        else if (quantity >= 4)
        {
            discount = unitPrice * quantity * 0.1m;
            message = $"✅ Desconto aplicado: 10% - Total com desconto: {unitPrice * quantity - discount:C}";
        }
        else
        {
            message = $"⚠️ Nenhum desconto aplicado - Total: {unitPrice * quantity:C}";
        }

        return (discount, message);
    }
}
