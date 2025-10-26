using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public sealed class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger;
        public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger) => _logger = logger;

        public Task Handle(SaleCreatedEvent notification, CancellationToken ct)
        {
            _logger.LogInformation("Sale created: {SaleId} at {When}", notification.SaleId, notification.OccurredOnUtc);
            return Task.CompletedTask;
        }
    }
}
