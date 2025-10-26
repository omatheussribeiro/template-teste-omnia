using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public sealed class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<SaleItem, SaleItemResult>();
            CreateMap<Sale, GetSaleResult>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));
        }
    }
}
