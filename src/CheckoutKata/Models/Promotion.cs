using System.Diagnostics.CodeAnalysis;

namespace CheckoutKata.Models
{
    [ExcludeFromCodeCoverage]
    public class Promotion
    {
        #region Properties

        public int PromotionId { get; set; }
        public string Description { get; set; }
        public int QuantityTrigger { get; set; }
        public decimal PromotionPrice { get; set; }
        public decimal PromotionPercentageReduction { get; set; }

        #endregion
    }
}