using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public sealed class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0m);
            RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
        }
    }
}
