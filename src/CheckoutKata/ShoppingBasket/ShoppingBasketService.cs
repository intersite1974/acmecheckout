using System;
using System.Collections.Generic;
using System.Linq;
using CheckoutKata.Models;
using CheckoutKata.PromotionEngine;
using CheckoutKata.ValidationEngine;

namespace CheckoutKata.ShoppingBasket
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        #region Fields

        private readonly IItemPriceCalculatorService _itemPriceCalculator;
        private readonly IValidationService _validationEngine;
        private Dictionary<Item, int> _basketItems;

        #endregion

        #region Constructor

        public ShoppingBasketService(IItemPriceCalculatorService itemPriceCalculator, IValidationService validationEngine)
        {
            _itemPriceCalculator = itemPriceCalculator;
            _validationEngine = validationEngine;
            _basketItems = new Dictionary<Item, int>();
        }

        #endregion

        #region Private Methods

        private bool AddOrUpdateBasketItem(Item attemptingItem, int quantity)
        {
            var existingItem = _basketItems.FirstOrDefault(i => i.Key.SkuId == attemptingItem.SkuId).Key;

            if (existingItem == null)
            {
                return _basketItems.TryAdd(attemptingItem, quantity);
            }
            else
            {
                var existingQty = _basketItems.FirstOrDefault(i => i.Key.SkuId == attemptingItem.SkuId).Value;
                var updatedQty = existingQty + quantity;
                _basketItems.Remove(existingItem);
                return _basketItems.TryAdd(attemptingItem, updatedQty);
            }
        }

        #endregion

        #region IShoppingBasket

        public bool TryAddItemToBasket(Item item, int quantity, out decimal updatedBasketTotal)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!_validationEngine.QuantityIsValid(quantity))
                throw new ArgumentOutOfRangeException(nameof(quantity));

            updatedBasketTotal = 0.00M;

            if (AddOrUpdateBasketItem(item, quantity))
            {
                updatedBasketTotal = TotalCostOfBasket();
                return true;
            }

            return false;
        }

        public int TotalItemLinesInBasket()
        {
            return _basketItems.Count;
        }

        public decimal TotalCostOfBasket()
        {
            if (TotalItemLinesInBasket() == 0) return 0.00M;

            var totalCostOfBasket = 0.00M;

            foreach (var basketItem in _basketItems)
            {
                var extraForCart = _itemPriceCalculator.CalculateItemPrice(basketItem.Key, basketItem.Value);
                totalCostOfBasket += extraForCart;
            }

            return Math.Round(totalCostOfBasket, 2);
        }

        #endregion
    }
}
