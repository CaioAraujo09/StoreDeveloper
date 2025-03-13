using SalesManagement.Domain.Events;

namespace SalesManagement.Domain.Interfaces
{
    public interface IEventPublisher
    {
        void Publish(IEvent @event);
    }
}