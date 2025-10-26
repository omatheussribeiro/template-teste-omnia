namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public sealed class ListSalesResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<SaleRow> Items { get; set; } = new();
    }

    public sealed class SaleRow
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
