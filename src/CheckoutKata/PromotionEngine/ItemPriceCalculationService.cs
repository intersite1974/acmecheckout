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

        #endregion

        #region Constructor

        public ItemPriceCalculatorService(IPromotionRepository promotionRepository, IValidationService validationEngine)
        {
            _promotionRepository = promotionRepository;
            _validationEngine = validationEngine;
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
                        if (itemPromotion.PromotionPercentageReduction > 0.00M)
                        {
                            // Percentage off promotion
                            //TODO
                        }
                        else
                        {
                            // X quantity break, X for "N"
                            // TODO
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

            return totalPrice;
        }

        #endregion
    }
}
