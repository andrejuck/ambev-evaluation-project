using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.StatusChangedSale
{
    public class SaleStatusChangedEventHandler : INotificationHandler<SaleStatusChangedDomainEvent>
    {
        private readonly ILogger<SaleStatusChangedEventHandler> _logger;

        public SaleStatusChangedEventHandler(ILogger<SaleStatusChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SaleStatusChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(notification.EventMessage());
        }
    }
}
