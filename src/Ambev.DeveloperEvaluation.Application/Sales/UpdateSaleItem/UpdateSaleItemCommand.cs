using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleItem
{
    public sealed class UpdateSaleItemCommand : IRequest<UpdateSaleItemResult>
    {
        public Guid ItemId { get; set; }
        public int NewQuantity { get; set; }
    }
}
