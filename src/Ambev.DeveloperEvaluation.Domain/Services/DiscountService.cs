namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Implementação da política:
    /// - qty < 4      => 0%
    /// - 4 <= qty <10 => 10%
    /// - 10 <= qty <=20 => 20%
    /// - qty > 20 ou <= 0 => inválido
    /// </summary>
    public class DiscountService : IDiscountService
    {
        public const int MaxQuantityPerProduct = 20;

        public decimal GetDiscountPercent(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

            if (quantity > MaxQuantityPerProduct)
                throw new ArgumentException($"O número máximo de unidades por produto é {MaxQuantityPerProduct}.", nameof(quantity));

            if (quantity < 4) return 0m;
            if (quantity < 10) return 10m;
            return 20m;
        }

        public bool TryGetDiscountPercent(int quantity, out decimal discountPercent)
        {
            discountPercent = 0m;

            if (quantity <= 0 || quantity > MaxQuantityPerProduct)
                return false;

            if (quantity < 4) { discountPercent = 0m; return true; }
            if (quantity < 10) { discountPercent = 10m; return true; }

            discountPercent = 20m;
            return true;
        }
    }
}
