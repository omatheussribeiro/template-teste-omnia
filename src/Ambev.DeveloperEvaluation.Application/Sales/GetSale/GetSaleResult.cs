using Ambev.DeveloperEvaluation.Application.Sales.Shared;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public sealed class GetSaleResult
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
        public List<SaleItemResult> Items { get; set; } = new();
    }
}
