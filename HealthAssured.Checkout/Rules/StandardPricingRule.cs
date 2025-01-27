namespace HealthAssured.Checkout.Rules;

public class StandardPricingRule : IPricingRule
{
    private readonly string _sku;
    private readonly decimal _unitPrice;

    public StandardPricingRule(string sku, decimal unitPrice)
    {
        _sku = sku;
        _unitPrice = unitPrice;
    }

    public bool AppliesTo(string sku) => _sku == sku;

    public decimal CalculatePrice(int quantity) => quantity * _unitPrice;
}