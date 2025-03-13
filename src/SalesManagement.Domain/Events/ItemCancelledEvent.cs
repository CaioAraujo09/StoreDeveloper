namespace SalesManagement.Domain.Events
{
    public class ItemCancelledEvent : IEvent
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public Guid SaleId { get; }
        public Guid ItemId { get; }

        public ItemCancelledEvent(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
