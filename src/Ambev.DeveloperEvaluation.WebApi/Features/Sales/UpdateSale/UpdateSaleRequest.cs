namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public sealed class UpdateSaleRequest
    {
        public List<UpdateSaleItemRequest> Items { get; set; } = new();
    }

    public sealed class UpdateSaleItemRequest
    {
        public Guid ItemId { get; set; }
        public int NewQuantity { get; set; }
    }
}
