using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public sealed class ItemCancelledEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid ItemId { get; }
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public ItemCancelledEvent(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
