namespace CheckoutKata.ValidationEngine
{
    public class ValidationService : IValidationService
    {
        #region IValidationEngine

        public bool QuantityIsValid(int quantity)
        {
            return quantity > 0;
        }

        #endregion
    }
}
