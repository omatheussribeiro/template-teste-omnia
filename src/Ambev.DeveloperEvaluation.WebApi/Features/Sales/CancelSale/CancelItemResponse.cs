namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public sealed class CancelItemResponse
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
        public decimal NewTotalAmount { get; set; }
    }
}
