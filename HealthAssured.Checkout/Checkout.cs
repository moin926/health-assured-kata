namespace HealthAssured.Checkout;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, PricingRule> _pricingRules;
    private readonly Dictionary<string, int> _items;

    public Checkout(IEnumerable<PricingRule> pricingRules)
    {
        _pricingRules = pricingRules.ToDictionary(r => r.SKU, r => r);
        _items = new Dictionary<string, int>();
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    public void Scan(string item)
    {
        if (!_pricingRules.ContainsKey(item))
            throw new ArgumentException($"Unknown SKU: {item}");
    }
}
