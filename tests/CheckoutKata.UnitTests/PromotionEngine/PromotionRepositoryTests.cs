using Xunit;
using FluentAssertions;
using CheckoutKata.PromotionEngine;
using CheckoutKata.Models;
using System;

namespace CheckoutKata.UnitTests.PromotionEngine
{
    public class PromotionRepositoryTests
    {
        #region Fields

        private readonly IPromotionRepository _sut;

        #endregion

        #region Constructor

        public PromotionRepositoryTests()
        {
            _sut = new PromotionRepository();
        }

        #endregion

        #region Tests

        [Fact]
        public void TryingToAddANullPromotion_Throws_ArgumentNullException()
        {
            // Act
            Action act = () => _sut.AddPromotion(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Promotion_IsAdded_OK()
        {
            // Arrange
            var promotionToBeAdded = new Promotion
            {
                PromotionId = 1,
                Description = "This is a test",
                PromotionPrice = 9.99M,
                QuantityTrigger = 3,
                PromotionPercentageReduction = 0.00M
            };

            // Act
            var result = _sut.AddPromotion(promotionToBeAdded);

            // Assert
            result.Should().Be(true);
        }

        #endregion
    }
}
