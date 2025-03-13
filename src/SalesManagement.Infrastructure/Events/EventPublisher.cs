using SalesManagement.Domain.Events;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly List<IEvent> _events = new();

        public void Publish(IEvent @event)
        {
            _events.Add(@event);
            Console.WriteLine($"Evento publicado: {@event.GetType().Name} em {@event.Timestamp}");
        }

        public List<IEvent> GetPublishedEvents() => _events;
    }
}