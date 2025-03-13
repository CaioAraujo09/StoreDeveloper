using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Domain.Events
{
    public class SaleCancelledEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public Guid SaleId { get; }

        public SaleCancelledEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
