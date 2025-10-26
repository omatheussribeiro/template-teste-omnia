using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public sealed class GetSaleQuery : IRequest<GetSaleResult>
    {
        public Guid SaleId { get; }
        public GetSaleQuery(Guid saleId) => SaleId = saleId;
    }
}
