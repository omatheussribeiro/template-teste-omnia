namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleItem
{
    public sealed class UpdateSaleItemResult
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
        public int NewQuantity { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal SaleTotalAmount { get; set; }
    }
}
