using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public sealed class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator(IDiscountService discountService)
        {
            RuleFor(x => x.SaleId).NotEmpty();
            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ItemId).NotEmpty();
                item.RuleFor(i => i.NewQuantity)
                    .Must(q => new MaxQuantityPerProductSpecification().IsSatisfiedBy(q))
                    .WithMessage(new MaxQuantityPerProductSpecification().ErrorMessage);
            });
        }
    }
}
