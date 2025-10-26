using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Application.Sales.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Mapping
{
    public sealed class SaleProfiles : Profile
    {
        public SaleProfiles()
        {
            CreateMap<SaleItem, SaleItemResult>();
            CreateMap<Sale, GetSaleResult>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));
            CreateMap<Sale, SaleRow>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));
        }
    }
}
