using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Valida se o desconto aplicado está coerente com a política de quantidade.
    /// </summary>
    public sealed class DiscountPolicySpecification : ISpecification<(int quantity, decimal discountPercent)>
    {
        private readonly IDiscountService _discountService;

        public DiscountPolicySpecification(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public string ErrorMessage => "O desconto informado não está coerente com a política de quantidade.";

        public bool IsSatisfiedBy((int quantity, decimal discountPercent) input)
        {
            var (quantity, discountPercent) = input;

            // usa o serviço de domínio para obter o desconto esperado
            var expected = _discountService.GetDiscountPercent(quantity);

            return discountPercent == expected;
        }
    }
}
