using SalesManagement.Domain.Events;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Infrasctructure.Events
{
    public class EventHandler
    {
        public void Handle(IEventPublisher @event)
        {
            switch (@event)
            {
                case SaleCreatedEvent saleCreated:
                    Console.WriteLine($"[Handler] Venda Criada - ID: {saleCreated.SaleId}, Total: {saleCreated.TotalAmount}");
                    break;

                case SaleCancelledEvent saleCancelled:
                    Console.WriteLine($"[Handler] Venda Cancelada - ID: {saleCancelled.SaleId}");
                    break;

                case SaleModifiedEvent saleModified:
                    Console.WriteLine($"[Handler] Venda Modificada - ID: {saleModified.SaleId}");
                    break;

                case ItemCancelledEvent itemCancelledEvent:
                    Console.WriteLine($"[Handler] Item Cancelado - ID: {itemCancelledEvent.ItemId}");
                    break;

                default:
                    Console.WriteLine("[Handler] Evento não reconhecido.");
                    break;
            }
        }
    }
}
