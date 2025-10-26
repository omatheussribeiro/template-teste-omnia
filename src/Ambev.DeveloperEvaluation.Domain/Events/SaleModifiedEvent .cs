using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    /// <summary>
    /// Disparado quando itens/quantidades/valores mudam e o total é recalculado.
    /// </summary>
    public sealed class SaleModifiedEvent : INotification
    {
        public Guid SaleId { get; }
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public SaleModifiedEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
