using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public sealed class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _repo;
        private readonly IDiscountService _discount;

        public UpdateSaleHandler(ISaleRepository repo, IDiscountService discount)
        {
            _repo = repo;
            _discount = discount;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken ct)
        {
            var sale = await _repo.GetByIdWithItemsAsync(request.SaleId, ct)
                       ?? throw new KeyNotFoundException("Venda não encontrada.");

            if (request.Items is null || request.Items.Count == 0)
                throw new ArgumentException("Nenhum item informado para atualização.");

            foreach (var i in request.Items)
            {
                // valida e aplica a nova quantidade (regra de desconto no domínio)
                sale.UpdateItemQuantity(_discount, i.ItemId, i.NewQuantity);
            }

            await _repo.UpdateAsync(sale, ct);

            return new UpdateSaleResult
            {
                SaleId = sale.Id,
                UpdatedItems = request.Items.Count,
                TotalAmount = sale.TotalAmount
            };
        }
    }
}
