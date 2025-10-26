using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public sealed class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator(IDiscountService discountService)
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.SaleDate).NotEqual(default(DateTime));
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ClientName).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Venda deve conter ao menos 1 item.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).NotEmpty();
                item.RuleFor(i => i.ProductName).NotEmpty();
                item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0m);

                // Max 20 por Specification
                item.RuleFor(i => i.Quantity)
                    .Must(q => new MaxQuantityPerProductSpecification().IsSatisfiedBy(q))
                    .WithMessage(new MaxQuantityPerProductSpecification().ErrorMessage);

                // Se a API aceitar DiscountPercent no request:
                item.RuleFor(i => i)
                    .Must(i => !i.DiscountPercent.HasValue ||
                               new DiscountPolicySpecification(discountService)
                                   .IsSatisfiedBy((i.Quantity, i.DiscountPercent.Value)))
                    .WithMessage("DiscountPercent incoerente com a política de quantidade.");
            });
        }
    }
}
