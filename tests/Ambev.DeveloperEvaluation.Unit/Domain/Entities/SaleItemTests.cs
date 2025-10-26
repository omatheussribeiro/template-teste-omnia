using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        private readonly DiscountService _discount = new();

        [Fact]
        public void NewItem_Qty3_Should_Have_0_Discount()
        {
            var saleId = Guid.NewGuid();
            var item = new SaleItem(saleId, Guid.NewGuid(), "Produto A", 100m, 3, _discount);

            Assert.Equal(0m, item.DiscountPercent);
            Assert.Equal(300m, item.Total);
        }

        [Fact]
        public void NewItem_Qty4_Should_Have_10_Discount()
        {
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Produto", 100m, 4, _discount);
            Assert.Equal(10m, item.DiscountPercent);
            Assert.Equal(360m, item.Total); // 4 * 100 * 0.9
        }

        [Fact]
        public void NewItem_Qty10_Should_Have_20_Discount()
        {
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Produto", 100m, 10, _discount);
            Assert.Equal(20m, item.DiscountPercent);
            Assert.Equal(800m, item.Total); // 10 * 100 * 0.8
        }

        [Fact]
        public void UpdateQuantity_To21_Should_Throw()
        {
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Produto", 100m, 10, _discount);
            Assert.Throws<ArgumentException>(() => item.UpdateQuantity(_discount, 21));
        }

        [Fact]
        public void Cancel_Should_Zero_Total()
        {
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Produto", 50m, 5, _discount);
            item.Cancel();
            Assert.True(item.IsCancelled);
            Assert.Equal(0m, item.Total);
        }
    }
}
