namespace HealthAssured.Checkout.Rules;

public class SpecialOfferPricingRule : IPricingRule
{
    private readonly string _sku;
    private readonly int _specialQuantity;
    private readonly decimal _specialPrice;
    private readonly decimal _unitPrice;

    public SpecialOfferPricingRule(string sku, int specialQuantity, decimal specialPrice, decimal unitPrice)
    {
        _sku = sku;
        _specialQuantity = specialQuantity;
        _specialPrice = specialPrice;
        _unitPrice = unitPrice;
    }

    public bool AppliesTo(string sku) => _sku == sku;

    public decimal CalculatePrice(int quantity)
    {
        decimal specialPriceUnits = (quantity / _specialQuantity) * _specialPrice;
        decimal regularPriceUnits = (quantity % _specialQuantity) * _unitPrice;

        return specialPriceUnits + regularPriceUnits;
    }
}