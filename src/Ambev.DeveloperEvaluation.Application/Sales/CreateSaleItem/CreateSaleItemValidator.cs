using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleItem
{
    public sealed class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemDtoValidator(IDiscountService discountService)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("O campo ProductId é obrigatório.");

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("O campo ProductName é obrigatório.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O preço unitário deve ser maior ou igual a zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade deve ser maior que zero.")
                .Must(q => new MaxQuantityPerProductSpecification().IsSatisfiedBy(q))
                .WithMessage(new MaxQuantityPerProductSpecification().ErrorMessage);

            // Caso o front envie DiscountPercent (ex: integração futura)
            When(x => x.DiscountPercent.HasValue, () =>
            {
                RuleFor(x => x)
                    .Must(x =>
                        new DiscountPolicySpecification(discountService)
                            .IsSatisfiedBy((x.Quantity, x.DiscountPercent.Value)))
                    .WithMessage("O desconto informado não está coerente com a política de quantidade.");
            });
        }
    }
}
