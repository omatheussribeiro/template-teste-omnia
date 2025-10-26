namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public sealed class ListSalesResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<ListSalesRowResponse> Items { get; set; } = new();
    }

    public sealed class ListSalesRowResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = "";
        public DateTime SaleDate { get; set; }
        public string ClientName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
    }
}
