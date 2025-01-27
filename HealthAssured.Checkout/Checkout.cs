namespace HealthAssured.Checkout;

public class Checkout : ICheckout
{
    private readonly PricingEngine _pricingEngine;
    private readonly Dictionary<string, int> _items;

    public Checkout(PricingEngine pricingEngine)
    {
        _pricingEngine = pricingEngine;
        _items = new Dictionary<string, int>();
    }

    public void Scan(string item)
    {
        if (!_pricingEngine.IsValidSku(item))
            throw new ArgumentException($"Unknown SKU: {item}");

        if (_items.ContainsKey(item))
            _items[item]++;
        else
            _items[item] = 1;
    }

    public decimal GetTotalPrice()
    {
        return _pricingEngine.CalculatePrice(_items);
    }
}
