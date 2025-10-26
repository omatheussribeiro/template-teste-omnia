namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public sealed class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
