using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public sealed class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<SaleItemResult, GetSaleItemResponse>();
        }
    }
}
