using System;
using Xunit;
using NSubstitute;
using FluentAssertions;
using CheckoutKata.ValidationEngine;
using CheckoutKata.PromotionEngine;
using CheckoutKata.Models;
using CheckoutKata.ShoppingBasket;

namespace CheckoutKata.UnitTests.ShoppingBasket
{
    public class ShoppingBasketServiceTests
    {
        #region Fields

        private readonly IShoppingBasketService _sut;
        private readonly IItemPriceCalculatorService _itemPriceCalculator;
        private readonly IValidationService _validationService;

        #endregion

        #region Constants

        private const string ItemSkuId = "A";
        private const int ItemPromotionId = 1;
        private const string ItemDescription = "Test Widget";
        private const decimal ItemUnitPrice = 9.99M;

        #endregion

        #region Constructor

        public ShoppingBasketServiceTests()
        {
            _itemPriceCalculator = Substitute.For<IItemPriceCalculatorService>();
            _validationService = Substitute.For<IValidationService>();

            _sut = new ShoppingBasketService(_itemPriceCalculator, _validationService);
        }

        #endregion

        #region Tests

        [Fact]
        public void AddingItemToBasket_WithNullItem_Fails()
        {
            // Act
            Action act = () => _sut.TryAddItemToBasket(null, 3, out _);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(-8888)]
        public void Adding_Item_With_InvalidQuantity_Fails(int quantity)
        {
            // Arrange
            var testItem = new Item
            {
                SkuId = ItemSkuId,
                PromotionId = ItemPromotionId,
                Description = ItemDescription,
                UnitPrice = ItemUnitPrice
            };

            // Act
            Action act = () => _sut.TryAddItemToBasket(testItem, quantity, out _);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion
    }
}
