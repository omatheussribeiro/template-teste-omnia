using Ambev.DeveloperEvaluation.Application.Sales.CancelItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public sealed class CancelItemProfile : Profile
    {
        public CancelItemProfile()
        {
            CreateMap<CancelItemResult, CancelItemResponse>();
        }
    }
}
