using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public sealed class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator(CreateSaleItemRequestValidator itemValidator)
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.SaleDate).NotEqual(default(DateTime));
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ClientName).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("A venda deve conter ao menos 1 item.");

            RuleForEach(x => x.Items).SetValidator(itemValidator);
        }
    }
}
