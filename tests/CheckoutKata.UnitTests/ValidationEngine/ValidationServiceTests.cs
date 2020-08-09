using Xunit;
using FluentAssertions;
using CheckoutKata.ValidationEngine;

namespace CheckoutKata.UnitTests.ValidationEngine
{
    public class ValidationServiceTests
    {
        #region Fields

        private readonly IValidationService _sut;

        #endregion

        #region Constructor

        public ValidationServiceTests()
        {
            _sut = new ValidationService();
        }

        #endregion

        #region Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(-8888)]
        public void BasketItem_InvalidQuantity_Fails(int quantity)
        {
            // Act
            var result = _sut.QuantityIsValid(quantity);

            // Assert
            result.Should().Be(false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(105)]
        [InlineData(1000)]
        [InlineData(11000)]
        public void BasketItem_ValidQuantity_OK(int quantity)
        {
            // Act
            var result = _sut.QuantityIsValid(quantity);

            // Assert
            result.Should().Be(true);
        }

        #endregion
    }
}
