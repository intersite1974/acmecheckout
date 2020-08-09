using CheckoutKata.Models;

namespace CheckoutKata.PromotionEngine
{
    public interface IPromotionRepository
    {
        bool AddPromotion(Promotion promotion);
        bool TryGetPromotion(int promotionId, out Promotion promotion);
    }
}