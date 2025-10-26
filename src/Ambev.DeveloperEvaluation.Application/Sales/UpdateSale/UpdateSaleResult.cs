namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public sealed class UpdateSaleResult
    {
        public Guid SaleId { get; set; }
        public int UpdatedItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
