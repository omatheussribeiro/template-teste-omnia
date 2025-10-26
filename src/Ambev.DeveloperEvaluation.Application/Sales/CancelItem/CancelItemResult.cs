namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem
{
    public sealed class CancelItemResult
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
        public decimal NewTotalAmount { get; set; }
    }
}
