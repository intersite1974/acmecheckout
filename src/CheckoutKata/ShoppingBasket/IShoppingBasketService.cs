using CheckoutKata.Models;

namespace CheckoutKata.ShoppingBasket
{
    public interface IShoppingBasketService
    {
        bool TryAddItemToBasket(Item item, int quantity, out decimal updatedBasketTotal);
        int TotalItemLinesInBasket();
        decimal TotalCostOfBasket();
    }
}