
using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.CreatedSale
{
    public class SaleCreatedDomainEvent : BaseDomainEvent<Entities.Sale>, INotification
    {
        public SaleCreatedDomainEvent(Entities.Sale sale)
        {
            EventId = Guid.NewGuid();
            Entity = sale;
            EventType = EventType.Creation;
        }

    }
}
