using CheckoutKata.Models;

namespace CheckoutKata.PromotionEngine
{
    public interface IItemPriceCalculatorService
    {
        decimal CalculateItemPrice(Item item, int quantity);
    }
}
