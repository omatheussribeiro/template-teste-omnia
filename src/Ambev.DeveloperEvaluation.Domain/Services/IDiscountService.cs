namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Define a política de desconto por quantidade.
    /// Retorna a porcentagem de desconto (0, 10 ou 20).
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Obtém a % de desconto aplicável para a quantidade informada.
        /// Lança exceção se a quantidade for inválida (> 20 ou <= 0).
        /// </summary>
        /// <param name="quantity">Quantidade do item.</param>
        /// <returns>Desconto em porcentagem (0, 10, 20).</returns>
        decimal GetDiscountPercent(int quantity);

        /// <summary>
        /// Tenta obter a % de desconto. Retorna false se quantidade inválida.
        /// </summary>
        bool TryGetDiscountPercent(int quantity, out decimal discountPercent);
    }
}
