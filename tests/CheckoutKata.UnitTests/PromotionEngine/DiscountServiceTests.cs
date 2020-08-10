using Xunit;
using FluentAssertions;
using CheckoutKata.PromotionEngine;

namespace CheckoutKata.UnitTests.PromotionEngine
{
    public class DiscountServiceTests
    {
        #region Constants

        #endregion

        #region Fields

        private readonly IDiscountService _sut;

        #endregion

        #region Constructor

        public DiscountServiceTests()
        {
            _sut = new DiscountService();
        }

        #endregion

        #region Tests

        [Theory]
        [InlineData(8.99, 9.99, 7.99)]
        [InlineData(6.00, 4.29, 7.71)]
        [InlineData(19.99, 15.99, 23.99)]

        public void PlainVolumeDiscount_OK(decimal unitPrice, decimal promotionPrice, decimal expectedValue)
        {
            // Act
            var result = _sut.PlainVolumeDiscount(unitPrice, promotionPrice);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(5, 4.99, 25.00, 1.99)]
        [InlineData(7, 3.99, 15.00, 1.99)]
        [InlineData(10, 14.99, 10.00, 1.99)]
        public void PercentageDiscount_OK(int quantity, decimal unitPrice, decimal promotionPercentage, decimal expectedValue)
        {
            // Act
            var result = _sut.PercentageDiscount(quantity, unitPrice, promotionPercentage);

            // Assert
            result.Should().Be(expectedValue);
        }

        #endregion
    }
}
