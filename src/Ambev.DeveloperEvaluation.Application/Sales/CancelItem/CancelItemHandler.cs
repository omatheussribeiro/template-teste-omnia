using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem
{
    public sealed class CancelItemHandler : IRequestHandler<CancelItemCommand, CancelItemResult>
    {
        private readonly ISaleRepository _repo;

        public CancelItemHandler(ISaleRepository repo) => _repo = repo;

        public async Task<CancelItemResult> Handle(CancelItemCommand request, CancellationToken ct)
        {
            var sale = await _repo.GetByIdWithItemsAsync(request.SaleId, ct)
                       ?? throw new KeyNotFoundException("Venda não encontrada.");

            if (sale.Items is not null)
            {
                var item = sale.Items.Where(x => x.Id == request.ItemId).FirstOrDefault();

                if (item is not null)
                {
                    item.Cancel();
                }
            }

            await _repo.UpdateAsync(sale, ct);

            return new CancelItemResult
            {
                SaleId = sale.Id,
                ItemId = request.ItemId,
                NewTotalAmount = sale.TotalAmount
            };
        }
    }
}
