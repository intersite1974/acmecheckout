namespace CheckoutKata.PromotionEngine
{
    public class DiscountService : IDiscountService
    {
        #region IDiscountService

        public decimal PlainVolumeDiscount(decimal unitPrice, decimal promotionPrice)
        {
            return unitPrice - (promotionPrice - unitPrice);
        }

        public decimal PercentageDiscount(int quantity, decimal unitPrice, decimal promotionPercentage)
        {
            return unitPrice - (promotionPercentage / 100) * (quantity * unitPrice);
        }

        #endregion
    }
}
