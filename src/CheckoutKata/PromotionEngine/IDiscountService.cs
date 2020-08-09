namespace CheckoutKata.PromotionEngine
{
    public interface IDiscountService
    {
        decimal PlainVolumeDiscount(decimal unitPrice, decimal promotionPrice);
        decimal PercentageDiscount(int quantity, decimal unitPrice, decimal promotionPercentage);
    }
}
