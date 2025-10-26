using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    /// <summary>
    /// Garante que a quantidade informada seja menor ou igual ao máximo permitido (<= 20).
    /// </summary>
    public sealed class MaxQuantityPerProductSpecification : ISpecification<int>
    {
        public string ErrorMessage => $"O número máximo de unidades por produto é {DiscountService.MaxQuantityPerProduct}.";

        public bool IsSatisfiedBy(int quantity)
        {
            return quantity > 0 && quantity <= DiscountService.MaxQuantityPerProduct;
        }
    }
}
