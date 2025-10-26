using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleItem;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public sealed class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid SaleId { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; } = new();
    }
}
