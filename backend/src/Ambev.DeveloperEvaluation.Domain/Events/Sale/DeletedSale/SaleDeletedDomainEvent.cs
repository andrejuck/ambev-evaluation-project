using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.DeletedSale
{
    public class SaleDeletedDomainEvent : BaseDomainEvent<Entities.Sale>, INotification
    {
        public SaleDeletedDomainEvent(Entities.Sale sale)
        {
            EventId = Guid.NewGuid();
            Entity = sale;
            EventType = EventType.Deletion;
        }
    }
}
