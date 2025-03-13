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
}
