using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public sealed class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string SaleNumber { get; set; } = "";
        public DateTime SaleDate { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = "";
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = "";
        public List<CreateSaleItemCommand> Items { get; set; } = new();
    }
}
