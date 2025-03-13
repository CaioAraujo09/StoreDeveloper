namespace SalesManagement.Domain.Events
{
    public interface IEvent
    {
        DateTime Timestamp { get; }
    }
}