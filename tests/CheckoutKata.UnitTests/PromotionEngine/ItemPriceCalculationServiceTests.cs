using System;
using Xunit;
using NSubstitute;
using FluentAssertions;
using CheckoutKata.PromotionEngine;
using CheckoutKata.ValidationEngine;
using CheckoutKata.Models;

namespace CheckoutKata.UnitTests.PromotionEngine
{
    public class ItemPriceCalculatorServiceTests
    {
        #region Constants

        private const string ItemSkuId = "A";
        private const int ItemPromotionId = 1;
        private const string ItemDescription = "Test Widget";
        private const decimal ItemUnitPrice = 9.99M;
        private const int ItemQuantity = 3;

        #endregion

        #region Fields

        private readonly IItemPriceCalculatorService _sut;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IValidationService _validationEngine;
        private readonly IDiscountService _discountService;

        #endregion

        #region Constructor

        public ItemPriceCalculatorServiceTests()
        {
            _promotionRepository = Substitute.For<IPromotionRepository>();
            _validationEngine = Substitute.For<IValidationService>();
            _discountService = Substitute.For<IDiscountService>();

            _sut = new ItemPriceCalculatorService(_promotionRepository, _validationEngine, _discountService);
        }

        #endregion

        #region Tests

        [Fact]
        public void AttemptingToGetItemPriceForNullItem_Throws_ArgumentNullException()
        {
            // Act
            Action act = () => _sut.CalculateItemPrice(null, ItemQuantity);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(-8888)]
        public void AttemptingToGetItemPriceForInvalidQuantity_Throws_ArgumentOutOfRangeException(int quantityToTest)
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
            Action act = () => _sut.CalculateItemPrice(testItem, quantityToTest);

            // Assert            
            _promotionRepository.DidNotReceiveWithAnyArgs().TryGetPromotion(testItem.PromotionId, out _);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1, 11.99)]
        [InlineData(10, 1001.99)]
        [InlineData(25, 13181.22)]
        public void AttemptingToGetItemPriceForItemWithDefaultOrNoneExistentPromotionCode_Returns_Standard_Selling_Price(int quantity,
            decimal standardPrice)
        {
            // Arrange
            var testItem = new Item
            {
                SkuId = ItemSkuId,
                PromotionId = 0,
                Description = ItemDescription,
                UnitPrice = standardPrice
            };

            _validationEngine.QuantityIsValid(quantity).Returns(true);
            _promotionRepository.TryGetPromotion(testItem.PromotionId, out _).Returns(false);

            // Act
            var returnedPrice = _sut.CalculateItemPrice(testItem, quantity);

            // Assert
            returnedPrice.Should().Be(quantity * standardPrice);
        }

        #endregion
    }
}
