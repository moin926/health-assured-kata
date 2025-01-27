namespace HealthAssured.Checkout.Tests;

public class CheckoutTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Scan_ShouldThrowException_ForUnknownSKU()
    {
        // Arrange
        var pricingRules = new List<PricingRule>
        {
            new PricingRule { SKU = "A", UnitPrice = 50 }
        };
        var checkout = new Checkout(pricingRules);

        // Assert
        Assert.Throws<ArgumentException>(() => checkout.Scan("Z"));

    }
}