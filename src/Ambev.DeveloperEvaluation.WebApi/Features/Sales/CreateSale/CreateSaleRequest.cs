using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public sealed class CreateSaleRequest
    {
        [Required] public string SaleNumber { get; set; } = string.Empty;
        [Required] public DateTime SaleDate { get; set; }

        [Required] public Guid ClientId { get; set; }
        [Required] public string ClientName { get; set; } = string.Empty;

        [Required] public Guid BranchId { get; set; }
        [Required] public string BranchName { get; set; } = string.Empty;

        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }
}
