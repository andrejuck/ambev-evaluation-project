using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale.DeletedSale
{
    public class SaleDeletedEventHandler : INotificationHandler<SaleDeletedDomainEvent>
    {
        private readonly ILogger<SaleDeletedEventHandler> _logger;

        public SaleDeletedEventHandler(ILogger<SaleDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SaleDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(notification.EventMessage());
        }
    }
}
