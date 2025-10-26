namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public sealed class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = "";
        public DateTime SaleDate { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = "";
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
        public List<GetSaleItemResponse> Items { get; set; } = new();
    }

    public sealed class GetSaleItemResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal Total { get; set; }
        public bool IsCancelled { get; set; }
    }
}
