namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public sealed class CreateSaleResult
    {
        public Guid SaleId { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Active";
    }
}
