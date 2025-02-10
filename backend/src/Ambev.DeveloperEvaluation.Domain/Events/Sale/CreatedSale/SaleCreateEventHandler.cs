

using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.CreatedSale
{
    public class SaleCreateEventHandler : INotificationHandler<SaleCreatedDomainEvent>
    {
        private readonly ILogger<SaleCreateEventHandler> _logger;

        public SaleCreateEventHandler(ILogger<SaleCreateEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SaleCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(notification.EventMessage());
        }
    }
}
