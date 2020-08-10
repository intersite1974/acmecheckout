using System;
using CheckoutKata.Models;
using CheckoutKata.ValidationEngine;

namespace CheckoutKata.PromotionEngine
{
    public class ItemPriceCalculatorService : IItemPriceCalculatorService
    {
        #region Fields

        private readonly IPromotionRepository _promotionRepository;
        private readonly IValidationService _validationEngine;
        private readonly IDiscountService _discountService;

        #endregion

        #region Constructor

        public ItemPriceCalculatorService(IPromotionRepository promotionRepository, IValidationService validationEngine, 
            IDiscountService discountService)
        {
            _promotionRepository = promotionRepository;
            _validationEngine = validationEngine;
            _discountService = discountService;
        }

        #endregion

        #region Private Methods

        private Promotion GetPromotionForItem(int promotionId)
        {
            var gotPromotion = _promotionRepository.TryGetPromotion(promotionId, out Promotion itemPromotion);
            return itemPromotion;
        }

        #endregion

        #region IItemPriceCalculator

        public decimal CalculateItemPrice(Item item, int quantity)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!_validationEngine.QuantityIsValid(quantity))
                throw new ArgumentOutOfRangeException(nameof(quantity));

            var itemPromotion = GetPromotionForItem(item.PromotionId);

            // If Promotion cannot be found, return standard price
            if (itemPromotion == null)
                return quantity * item.UnitPrice;

            var totalPrice = 0.00M;

            if (itemPromotion.QuantityTrigger > 0)
            {
                var qtyBreakCounter = 1;

                for (var i = 1; i <= quantity; i++)
                {
                    if (qtyBreakCounter == itemPromotion.QuantityTrigger)
                    {
                        // Here, a refinement could be some form of Strategy pattern to detect the appropriate
                        // relevent discount algorithm to use.. For now we will use how we have set the promotion fields
                        if (itemPromotion.PromotionPercentageReduction > 0.00M)
                        {
                            // Percentage off promotion by percentage
                            totalPrice -= _discountService.PercentageDiscount(itemPromotion.QuantityTrigger, item.UnitPrice, itemPromotion.PromotionPercentageReduction);
                        }
                        else
                        {
                            // X quantity break, X for "N"
                            totalPrice -= _discountService.PlainVolumeDiscount(item.UnitPrice, itemPromotion.PromotionPrice);
                        }

                        qtyBreakCounter = 1;
                    }
                    else
                    {
                        totalPrice += item.UnitPrice;
                        qtyBreakCounter++;
                    }
                }
            }

            return Math.Round(totalPrice, 2);
        }

        #endregion
    }
}
