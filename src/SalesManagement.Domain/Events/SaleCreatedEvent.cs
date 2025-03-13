using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Domain.Events
{
    public class SaleCreatedEvent : IEvent
    {
        public Guid SaleId { get; }
        public decimal TotalAmount { get; }
        public List<SaleItem> Items { get; } = new();
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public SaleCreatedEvent(Guid saleId, decimal totalAmount, List<SaleItem> items)
        {
            SaleId = saleId;
            TotalAmount = totalAmount;
            Items = items ?? new List<SaleItem>();
        }
    }
}