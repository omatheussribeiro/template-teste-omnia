using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem
{
    public sealed class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
    {
        private readonly ISaleRepository _repo;
        private readonly IDiscountService _discount;

        public CreateSaleItemHandler(ISaleRepository repo, IDiscountService discount)
        {
            _repo = repo;
            _discount = discount;
        }

        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand request, CancellationToken ct)
        {
            var sale = await _repo.GetByIdWithItemsAsync(request.Id, ct)
                       ?? throw new KeyNotFoundException("Venda não encontrada.");

            // Adiciona ou consolida quantidade (regra no agregado)
            sale.AddItem(_discount, request.ProductId, request.ProductName, request.UnitPrice, request.Quantity);

            await _repo.UpdateAsync(sale, ct);

            // Descobre o item afetado (pode ser o existente consolidado)
            var affectedItem = sale.Items
                .Where(i => !i.IsCancelled && i.ProductId == request.ProductId)
                .OrderByDescending(i => i.Quantity) // heurística simples
                .First();

            return new CreateSaleItemResult
            {
                SaleId = sale.Id,
                AffectedItemId = affectedItem.Id,
                Quantity = affectedItem.Quantity,
                ItemTotal = affectedItem.Total,
                SaleTotalAmount = sale.TotalAmount
            };
        }
    }
}
