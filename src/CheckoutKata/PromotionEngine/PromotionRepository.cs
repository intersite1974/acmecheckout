using System;
using System.Collections.Generic;
using System.Linq;
using CheckoutKata.Models;

namespace CheckoutKata.PromotionEngine
{
    public class PromotionRepository : IPromotionRepository
    {
        #region Fields

        private IList<Promotion> _promotions;

        #endregion

        #region Constructor

        public PromotionRepository()
        {
            _promotions = new List<Promotion>();
        }

        #endregion

        #region IPromotionRepository

        public bool AddPromotion(Promotion promotion)
        {
            if (promotion == null)
                throw new ArgumentNullException(nameof(promotion));

            _promotions.Add(promotion);
            return true;
        }

        public bool TryGetPromotion(int promotionId, out Promotion promotion)
        {
            if (promotionId == 0)
            {
                promotion = null;
                return false;
            }

            promotion = _promotions.Where(i => i.PromotionId == promotionId).FirstOrDefault();

            return promotion != null;
        }

        #endregion
    }
}
