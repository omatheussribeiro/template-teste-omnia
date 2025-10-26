namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public sealed class CancelSaleResponse
    {
        public Guid SaleId { get; set; }
        public string Status { get; set; } = "Cancelled";
    }
}
