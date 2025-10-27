using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public sealed class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _repo;

        public CancelSaleHandler(ISaleRepository repo) => _repo = repo;

        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken ct)
        {
            var sale = await _repo.GetByIdWithItemsAsync(request.SaleId, ct)
                       ?? throw new KeyNotFoundException("Venda não encontrada.");

            sale.CancelSale(sale.Id);

            if (sale.Items is not null)
            {
                foreach (var item in sale.Items)
                {
                    if (!item.IsCancelled)
                    {
                        item.Cancel();
                    }
                }
            }

            await _repo.UpdateAsync(sale, ct);
            return new CancelSaleResult { SaleId = sale.Id, Status = sale.Status.ToString() };
        }
    }
}
