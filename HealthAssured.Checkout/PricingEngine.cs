namespace HealthAssured.Checkout;

public class PricingEngine
{
    private readonly List<IPricingRule> _pricingRules;

    public PricingEngine(IEnumerable<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules.ToList();
    }

    public bool IsValidSku(string sku) => _pricingRules.Any(rule => rule.AppliesTo(sku));

    public decimal CalculatePrice(Dictionary<string, int> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            var rule = _pricingRules.FirstOrDefault(r => r.AppliesTo(item.Key));
            if (rule != null)
            {
                total += rule.CalculatePrice(item.Value);
            }
        }

        return total;
    }
}
