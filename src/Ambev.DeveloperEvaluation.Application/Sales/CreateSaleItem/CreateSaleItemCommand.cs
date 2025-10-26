using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem
{
    public sealed class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? DiscountPercent { get; set; }
    }
}
