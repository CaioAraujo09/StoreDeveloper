using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Domain.Events
{
    public class SaleModifiedEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public Guid SaleId { get; }
        public decimal NewTotalAmount { get; }

        public SaleModifiedEvent(Guid saleId, decimal newTotalAmount)
        {
            SaleId = saleId;
            NewTotalAmount = newTotalAmount;
        }
    }
}
