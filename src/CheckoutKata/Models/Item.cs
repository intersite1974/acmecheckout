using System.Diagnostics.CodeAnalysis;

namespace CheckoutKata.Models
{
    [ExcludeFromCodeCoverage]
    public class Item
    {
        #region Properties

        public string SkuId { get; set; }
        public int PromotionId { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

        #endregion
    }
}