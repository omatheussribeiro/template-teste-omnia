using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public sealed class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _repo;
        private readonly IDiscountService _discount;

        public CreateSaleHandler(ISaleRepository repo, IDiscountService discount)
        {
            _repo = repo;
            _discount = discount;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken ct)
        {
            // Cria o agregado
            var sale = new Sale(
                id: Guid.NewGuid(),
                saleNumber: request.SaleNumber,
                saleDate: request.SaleDate,
                clientId: request.ClientId,
                clientName: request.ClientName,
                branchId: request.BranchId,
                branchName: request.BranchName
            );

            // Adiciona itens (se houver no command)
            if (request.Items is not null)
            {
                foreach (var i in request.Items)
                {
                    sale.AddItem(_discount, i.ProductId, i.ProductName, i.UnitPrice, i.Quantity);
                }
            }

            await _repo.AddAsync(sale, ct); // SaveChangesAsync despacha DomainEvents no DbContext

            return new CreateSaleResult
            {
                SaleId = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                Status = sale.Status.ToString()
            };
        }
    }
}
