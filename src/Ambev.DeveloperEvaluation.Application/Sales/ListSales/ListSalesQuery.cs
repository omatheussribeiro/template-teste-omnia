using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public sealed class ListSalesQuery : IRequest<ListSalesResult>
    {
        public string? SaleNumber { get; init; }
        public DateTime? DateFrom { get; init; }
        public DateTime? DateTo { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
