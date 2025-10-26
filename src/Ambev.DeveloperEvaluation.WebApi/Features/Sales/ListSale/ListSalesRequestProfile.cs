using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public sealed class ListSalesRequestProfile : Profile
    {
        public ListSalesRequestProfile()
        {
            CreateMap<ListSalesRequest, ListSalesQuery>();
        }
    }
}
