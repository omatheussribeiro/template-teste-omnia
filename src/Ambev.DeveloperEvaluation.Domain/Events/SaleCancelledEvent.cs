using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public sealed class SaleCancelledEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid ItemId { get; }
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public SaleCancelledEvent(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
