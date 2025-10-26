namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public sealed class CancelSaleResult
    {
        public Guid SaleId { get; set; }
        public string Status { get; set; } = "Cancelled";
    }
}
