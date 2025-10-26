using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public sealed class SaleCreatedEvent : INotification
    {
        public Guid SaleId { get; }
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public SaleCreatedEvent(Guid saleId) => SaleId = saleId;
    }
}
