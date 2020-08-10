using CheckoutKata.ShoppingBasket;
using CheckoutKata.PromotionEngine;
using CheckoutKata.ValidationEngine;
using CheckoutKata.Models;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var validationEngine = new ValidationService();
            var discountService = new DiscountService();
            var promotionRepository = new PromotionRepository();
            var itemPriceCalculator = new ItemPriceCalculatorService(promotionRepository, validationEngine, discountService);

            // Add Promotions
            promotionRepository.AddPromotion(new Promotion { PromotionId = 1, Description = "'X' for 'N' Promotion", PromotionPrice = 40.00M, QuantityTrigger = 3, PromotionPercentageReduction = 0.00M });
            promotionRepository.AddPromotion(new Promotion { PromotionId = 2, Description = "'X'% off for every 'N' purchased together Promotion", PromotionPrice = 40.00M, QuantityTrigger = 2, PromotionPercentageReduction = 25.00M });

            // Prep. Items
            var testItem1 = new Item { SkuId = "A", PromotionId = 0, Description = "Test Widget 1", UnitPrice = 10.00M };
            var testItem2 = new Item { SkuId = "B", PromotionId = 1, Description = "Test Widget 2", UnitPrice = 15.00M };
            var testItem3 = new Item { SkuId = "C", PromotionId = 0, Description = "Test Widget 3", UnitPrice = 40.00M };
            var testItem4 = new Item { SkuId = "D", PromotionId = 2, Description = "Test Widget 4", UnitPrice = 55.00M };

            var shoppingBasket = new ShoppingBasketService(itemPriceCalculator, validationEngine);

            // Add Sample Items to Basket
            var cartGrandTotal = 0.00M;

            // A
            shoppingBasket.TryAddItemToBasket(testItem1, 8, out var cartSubTotal1);
            cartGrandTotal = cartSubTotal1;

            // B
            shoppingBasket.TryAddItemToBasket(testItem2, 7, out var cartSubTotal2);
            cartGrandTotal = cartSubTotal2;

            // C
            shoppingBasket.TryAddItemToBasket(testItem3, 5, out var cartSubTotal3);
            cartGrandTotal = cartSubTotal3;

            // D
            shoppingBasket.TryAddItemToBasket(testItem4, 3, out var cartSubTotal4);
            cartGrandTotal = cartSubTotal4;

            Console.WriteLine($"Total Cost of Basket: £{cartGrandTotal}");
            Console.WriteLine("Press any key to exit the application..");
            Console.ReadKey(false);
        }
    }
}
