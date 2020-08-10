namespace CheckoutKata.PromotionEngine
{
    /// <summary>
    ///  TODO: A switch to a pattern that allows selection of the appropriate discount algorithm would be better for production
    ///  later (i.e. Strategy). Here we provide the idea of the discount algorithms being split out and put into a separate service to demo
    ///  SRP.
    /// </summary>
    public class DiscountService : IDiscountService
    {
        #region IDiscountService

        public decimal PlainVolumeDiscount(decimal unitPrice, decimal promotionPrice)
        {
            return unitPrice - (promotionPrice - unitPrice);
        }

        public decimal PercentageDiscount(int quantity, decimal unitPrice, decimal promotionPercentage)
        {
            var totalUnitPriceExcDiscount = quantity * unitPrice;
            return unitPrice - ((promotionPercentage / 100) * totalUnitPriceExcDiscount);
        }

        #endregion
    }
}
