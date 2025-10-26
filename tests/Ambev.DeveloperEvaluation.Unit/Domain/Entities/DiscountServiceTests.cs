using Ambev.DeveloperEvaluation.Domain.Services;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _svc = new();

        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 0)]
        [InlineData(4, 10)]
        [InlineData(9, 10)]
        [InlineData(10, 20)]
        [InlineData(20, 20)]
        public void GetDiscountPercent_Should_Return_Expected(int qty, decimal expected)
        {
            var result = _svc.GetDiscountPercent(qty);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(21)]
        public void GetDiscountPercent_Should_Throw_On_Invalid_Quantity(int qty)
        {
            Assert.Throws<ArgumentException>(() => _svc.GetDiscountPercent(qty));
        }

        [Fact]
        public void TryGetDiscountPercent_Should_BeFalse_On_Invalid()
        {
            var ok = _svc.TryGetDiscountPercent(0, out var _);
            Assert.False(ok);
        }

        [Fact]
        public void TryGetDiscountPercent_Should_Return_Value_On_Valid()
        {
            var ok = _svc.TryGetDiscountPercent(5, out var percent);
            Assert.True(ok);
            Assert.Equal(10m, percent);
        }
    }
}
