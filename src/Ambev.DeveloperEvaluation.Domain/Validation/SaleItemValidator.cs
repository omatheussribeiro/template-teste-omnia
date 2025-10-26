using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public sealed class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator(IDiscountService discount)
        {
            RuleFor(x => x).Must(i => i.DiscountPercent == discount.GetDiscountPercent(i.Quantity))
                           .WithMessage("DiscountPercent incoerente...");
        }
    }
}
