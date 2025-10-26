namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem
{
    public sealed class CreateSaleItemResult
    {
        public Guid SaleId { get; set; }
        public Guid AffectedItemId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal SaleTotalAmount { get; set; }
    }
}
