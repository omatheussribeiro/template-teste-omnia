using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public sealed class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator(IDiscountService discount)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.SaleDate).NotEqual(default(DateTime)).WithMessage("SaleDate inválida.");

            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ClientName).NotEmpty();

            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Items não pode ser nulo.")
                .Must(items => items.Any(i => !i.IsCancelled))
                .WithMessage("Venda deve ter ao menos 1 item não cancelado.");

            RuleForEach(x => x.Items)
                .SetValidator(new SaleItemValidator(discount));

            RuleFor(x => x.TotalAmount)
                .Must((sale, total) => total == sale.Items.Where(i => !i.IsCancelled).Sum(i => i.Total))
                .WithMessage("TotalAmount inconsistente com os itens.");
        }
    }
}
