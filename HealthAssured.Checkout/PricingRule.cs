namespace HealthAssured.Checkout;

public class PricingRule
{
    public required string SKU { get; set; }
    public required decimal UnitPrice { get; set; }
    public int? SpecialQuantity { get; set; }
    public decimal? SpecialPrice { get; set; }
}
