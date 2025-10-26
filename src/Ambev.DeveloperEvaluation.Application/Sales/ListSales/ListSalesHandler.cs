using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public sealed class ListSalesHandler : IRequestHandler<ListSalesQuery, ListSalesResult>
    {
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;

        public ListSalesHandler(ISaleRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<ListSalesResult> Handle(ListSalesQuery request, CancellationToken ct)
        {
            var (items, total) = await _repo.ListAsync(
                request.DateFrom, request.DateTo, request.SaleNumber,
                request.Page, request.PageSize, ct);

            return new ListSalesResult
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = total,
                Items = items.Select(_mapper.Map<SaleRow>).ToList()
            };
        }
    }
}
