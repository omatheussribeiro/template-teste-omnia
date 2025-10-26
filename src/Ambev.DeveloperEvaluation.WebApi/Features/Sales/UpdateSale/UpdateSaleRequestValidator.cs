using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public sealed class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Items).NotEmpty()
                .WithMessage("Informe ao menos um item para atualizar.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ItemId).NotEmpty();
                item.RuleFor(i => i.NewQuantity).GreaterThan(0).LessThanOrEqualTo(20);
            });
        }
    }
}
