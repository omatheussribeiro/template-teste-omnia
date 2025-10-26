namespace Ambev.DeveloperEvaluation.Application.Sales.Shared
{
    public sealed class SaleItemResult
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
