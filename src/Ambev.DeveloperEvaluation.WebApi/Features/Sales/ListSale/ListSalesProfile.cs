using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public sealed class ListSalesProfile : Profile
    {
        public ListSalesProfile()
        {
            CreateMap<ListSalesResult, ListSalesResponse>();
            CreateMap<SaleRow, ListSalesRowResponse>();
        }
    }
}
