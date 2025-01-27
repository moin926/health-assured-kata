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

    public void Scan(string item)
    {
        if (!_pricingRules.ContainsKey(item))
            throw new ArgumentException($"Unknown SKU: {item}");

        if (_items.ContainsKey(item))
            _items[item]++;
        else
            _items[item] = 1;
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;

        foreach (var item in _items)
        {
            var sku = item.Key;
            var quantity = item.Value;
            var rule = _pricingRules[sku];

            if (rule.SpecialQuantity.HasValue && rule.SpecialPrice.HasValue)
            {
                total += (quantity / rule.SpecialQuantity.Value) * rule.SpecialPrice.Value;
                total += (quantity % rule.SpecialQuantity.Value) * rule.UnitPrice;
            }
            else
                total += quantity * rule.UnitPrice;
        }

        return total;
    }
}
