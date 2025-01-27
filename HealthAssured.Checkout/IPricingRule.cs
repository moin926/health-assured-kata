namespace HealthAssured.Checkout;

public interface IPricingRule
{
    bool AppliesTo(string sku);

    decimal CalculatePrice(int quantity);
}
