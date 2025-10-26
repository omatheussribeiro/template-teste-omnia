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

            foreach (var item in sale.Items)
            {
                sale.CancelSale(item.Id);
            }

            await _repo.UpdateAsync(sale, ct);
            // SaveChangesAsync será chamado no nível do Unit of Work / Handler externo, caso tenha.
            return new CancelSaleResult { SaleId = sale.Id, Status = sale.Status.ToString() };
        }
    }
}
