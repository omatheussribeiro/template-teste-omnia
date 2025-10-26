using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        private readonly DiscountService _discount = new();

        private Sale NewSale()
        {
            return new Sale(
                new Guid(),
                saleNumber: "V-0001",
                saleDate: DateTime.UtcNow,
                clientId: Guid.NewGuid(),
                clientName: "Cliente XYZ",
                branchId: Guid.NewGuid(),
                branchName: "Filial Centro");
        }

        [Fact]
        public void AddItem_Should_Sum_Totals_With_Discounts()
        {
            var sale = NewSale();

            sale.AddItem(_discount, Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Produto A", 100m, 3); // 0% -> 300
            sale.AddItem(_discount, Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Produto B", 50m, 10);  // 20% -> 400

            Assert.Equal(700m, sale.TotalAmount);
            Assert.Equal(2, sale.Items.Count);
        }

        [Fact]
        public void AddItem_SameProduct_Should_Merge_Quantity_And_Reapply_Discount()
        {
            var sale = NewSale();
            var productId = Guid.NewGuid();

            sale.AddItem(_discount, productId, "Produto", 100m, 4); // 10% -> 360
            sale.AddItem(_discount, productId, "Produto", 100m, 6); // total qty=10 -> 20%

            var item = sale.Items.Single();
            Assert.Equal(10, item.Quantity);
            Assert.Equal(20m, item.DiscountPercent);
            Assert.Equal(800m, item.Total);
            Assert.Equal(800m, sale.TotalAmount);
        }

        [Fact]
        public void UpdateItemQuantity_Should_Recalculate_Total_And_Sale_Total()
        {
            var sale = NewSale();
            var productId = Guid.NewGuid();

            sale.AddItem(_discount, productId, "Produto", 200m, 4); // 10% -> 720
            var itemId = sale.Items.Single().Id;

            sale.UpdateItemQuantity(_discount, itemId, 3); // 0% -> 600

            Assert.Equal(600m, sale.TotalAmount);
            var item = sale.Items.Single();
            Assert.Equal(3, item.Quantity);
            Assert.Equal(0m, item.DiscountPercent);
            Assert.Equal(600m, item.Total);
        }

        [Fact]
        public void CancelItem_Should_Zero_Item_And_Update_Sale_Total()
        {
            var sale = NewSale();
            var p1 = Guid.NewGuid();
            var p2 = Guid.NewGuid();

            sale.AddItem(_discount, p1, "A", 100m, 4); // 360
            sale.AddItem(_discount, p2, "B", 50m, 3);  // 150

            var itemToCancel = sale.Items.First(i => i.ProductId == p1);
            sale.CancelItem(itemToCancel.Id);

            Assert.Equal(150m, sale.TotalAmount);
            Assert.True(sale.Items.First(i => i.ProductId == p1).IsCancelled);
        }

        [Fact]
        public void CancelSale_Should_Block_Mutations()
        {
            var sale = NewSale();
            sale.CancelSale(Guid.NewGuid());

            Assert.Throws<InvalidOperationException>(() =>
                sale.AddItem(_discount, Guid.NewGuid(), "Produto", 10m, 1));

            Assert.Throws<InvalidOperationException>(() =>
                sale.UpdateItemQuantity(_discount, Guid.NewGuid(), 5));
        }
    }
}
