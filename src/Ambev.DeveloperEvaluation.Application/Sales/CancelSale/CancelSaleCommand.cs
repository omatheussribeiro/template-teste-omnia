using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public sealed class CancelSaleCommand : IRequest<CancelSaleResult>
    {
        public Guid SaleId { get; }
        public CancelSaleCommand(Guid saleId) => SaleId = saleId;
    }
}
