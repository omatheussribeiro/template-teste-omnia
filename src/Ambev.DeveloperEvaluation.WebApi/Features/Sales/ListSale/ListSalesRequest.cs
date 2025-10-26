namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public sealed class ListSalesRequest
    {
        public string? SaleNumber { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
