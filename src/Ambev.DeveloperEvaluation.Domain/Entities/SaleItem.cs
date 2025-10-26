using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;

        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        /// <summary>Desconto em % (0, 10, 20)</summary>
        public decimal DiscountPercent { get; private set; }
        public decimal Total { get; private set; }
        public bool IsCancelled { get; private set; }

        // EF
        protected SaleItem() { }

        public SaleItem(Guid saleId, Guid productId, string productName, decimal unitPrice, int quantity, IDiscountService discountService)
        {
            if (discountService is null) throw new ArgumentNullException(nameof(discountService));
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("Nome do produto é obrigatório.", nameof(productName));
            if (unitPrice < 0) throw new ArgumentException("Preço unitário inválido.", nameof(unitPrice));

            SaleId = saleId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;

            ApplyQuantityAndDiscount(discountService, quantity);
        }

        public void UpdateQuantity(IDiscountService discountService, int newQuantity)
        {
            if (discountService is null) throw new ArgumentNullException(nameof(discountService));
            if (IsCancelled)
                throw new InvalidOperationException("Não é possível alterar um item cancelado.");

            ApplyQuantityAndDiscount(discountService, newQuantity);
        }

        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("O item já está cancelado.");

            IsCancelled = true;
            RecalculateTotal();
        }

        private void ApplyQuantityAndDiscount(IDiscountService discountService, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

            var qtySpec = new MaxQuantityPerProductSpecification();
            if (!qtySpec.IsSatisfiedBy(quantity))
                throw new DomainException(qtySpec.ErrorMessage);

            // Usa a política do domínio para definir o desconto
            var percent = discountService.GetDiscountPercent(quantity);

            Quantity = quantity;
            DiscountPercent = percent;
            IsCancelled = false; // garantir consistência ao reusar método
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            if (IsCancelled)
            {
                Total = 0m;
                return;
            }

            var discountFactor = 1 - (DiscountPercent / 100m);
            Total = Math.Round(Quantity * UnitPrice * discountFactor, 2);
        }
    }
}
