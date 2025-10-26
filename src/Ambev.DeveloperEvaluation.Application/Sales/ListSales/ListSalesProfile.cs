using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public sealed class ListSalesProfile : Profile
    {
        public ListSalesProfile()
        {
            CreateMap<Sale, SaleRow>()
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));
        }
    }
}
