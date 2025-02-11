
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.UpdatedSale
{
    public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedDomainEvent>
    {
        private readonly ILogger<SaleModifiedEventHandler> _logger;

        public SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SaleModifiedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(notification.EventMessage());
        }
    }
}
