namespace HealthAssured.Checkout;

public class Checkout : ICheckout
{
    private readonly List<IPricingRule> _pricingRules;
    private readonly Dictionary<string, int> _items;

    public Checkout(IEnumerable<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules.ToList();
        _items = new Dictionary<string, int>();
    }

    public void Scan(string item)
    {
        if (!_pricingRules.Any(rule => rule.AppliesTo(item)))
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
            var rule = _pricingRules.FirstOrDefault(r => r.AppliesTo(item.Key));
            if (rule != null)
            {
                total += rule.CalculatePrice(item.Value);
            }
        }

        return total;
    }
}
