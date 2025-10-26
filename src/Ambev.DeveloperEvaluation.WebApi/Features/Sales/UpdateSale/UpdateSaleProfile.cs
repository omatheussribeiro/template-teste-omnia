using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public sealed class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }

    public sealed class UpdateSaleResponse
    {
        public Guid SaleId { get; set; }
        public int UpdatedItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
