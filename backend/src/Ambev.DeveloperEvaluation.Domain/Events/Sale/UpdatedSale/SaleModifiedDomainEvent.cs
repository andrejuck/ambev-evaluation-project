using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.UpdatedSale
{
    public class SaleModifiedDomainEvent : BaseDomainEvent<Entities.Sale>, INotification
    {
        public SaleModifiedDomainEvent(Entities.Sale sale)
        {
            EventId = Guid.NewGuid();
            Entity = sale;
            EventType = EventType.Modification;
        }
    }
}
