
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.StatusChangedSale
{
    public class SaleStatusChangedDomainEvent : BaseDomainEvent<Entities.Sale>, INotification
    {
        public SaleStatusChangedDomainEvent(Entities.Sale sale, SaleStatus oldStatus)
        {
            EventId = Guid.NewGuid();
            Entity = sale;
            EventType = EventType.StatusChange;
            OldStatus = oldStatus;
        }

        public SaleStatus OldStatus { get; private set; }

        public override string EventMessage()
            => string.Format("EventType: {0}| EventId: {1} | PreviousStatus: {2} | NewStatus: {3} | EntityId: {4}",
                EventType,
                EventId,
                OldStatus,
                Entity.SaleStatus,
                Entity.Id);

    }
}
