using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleItem
{
    public sealed class UpdateSaleItemHandler : IRequestHandler<UpdateSaleItemCommand, UpdateSaleItemResult>
    {
        private readonly ISaleRepository _repo;
        private readonly IDiscountService _discount;

        public UpdateSaleItemHandler(ISaleRepository repo, IDiscountService discount)
        {
            _repo = repo;
            _discount = discount;
        }

        public async Task<UpdateSaleItemResult> Handle(UpdateSaleItemCommand request, CancellationToken ct)
        {
            var sale = await _repo.GetByIdWithItemsAsync(request.ItemId, ct)
                       ?? throw new KeyNotFoundException("Venda não encontrada.");

            sale.UpdateItemQuantity(_discount, request.ItemId, request.NewQuantity);

            await _repo.UpdateAsync(sale, ct);

            var item = sale.Items.First(i => i.Id == request.ItemId);

            return new UpdateSaleItemResult
            {
                SaleId = sale.Id,
                ItemId = item.Id,
                NewQuantity = item.Quantity,
                ItemTotal = item.Total,
                SaleTotalAmount = sale.TotalAmount
            };
        }
    }
}
