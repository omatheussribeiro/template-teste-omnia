using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public sealed class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
    {
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;

        public GetSaleHandler(ISaleRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken ct)
        {
            var sale = await _repo.GetByIdWithItemsAsync(request.SaleId, ct)
                       ?? throw new KeyNotFoundException("Venda não encontrada.");

            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}
