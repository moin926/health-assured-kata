namespace HealthAssured.Checkout.Tests;

public class CheckoutTests
{
    [Test]
    public void Scan_ShouldThrowException_ForUnknownSKU()
    {
        // Arrange
        var pricingRules = new List<PricingRule>
        {
            new PricingRule { SKU = "A", UnitPrice = 50 }
        };
        var checkout = new Checkout(pricingRules);

        // Act
        TestDelegate act = () => checkout.Scan("Z");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Test]
    public void GetTotalPrice_ShouldReturnCorrectPrice_WithoutSpecialOffers()
    {
        // Arrange
        var pricingRules = new List<PricingRule>
        {
            new PricingRule { SKU = "A", UnitPrice = 50 },
            new PricingRule { SKU = "B", UnitPrice = 30 }
        };
        var checkout = new Checkout(pricingRules);

        checkout.Scan("A");
        checkout.Scan("B");

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        Assert.That(total, Is.EqualTo(80D));
    }

    [Test]
    public void GetTotalPrice_ShouldApplySpecialOffer_WhenConditionsMet()
    {
        // Arrange
        var pricingRules = new List<PricingRule>
        {
            new PricingRule { SKU = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
        };
        var checkout = new Checkout(pricingRules);

        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("A");

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        Assert.That(total, Is.EqualTo(130D));
    }
}