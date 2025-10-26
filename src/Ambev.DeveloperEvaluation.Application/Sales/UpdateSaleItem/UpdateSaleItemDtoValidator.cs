using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleItem
{
    public sealed class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.ItemId)
                .NotEmpty()
                .WithMessage("O campo ItemId é obrigatório.");

            RuleFor(x => x.NewQuantity)
                .GreaterThan(0)
                .WithMessage("A quantidade deve ser maior que zero.")
                .Must(q => new MaxQuantityPerProductSpecification().IsSatisfiedBy(q))
                .WithMessage(new MaxQuantityPerProductSpecification().ErrorMessage);
        }
    }
}
