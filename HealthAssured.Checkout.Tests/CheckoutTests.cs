using HealthAssured.Checkout.Rules;

namespace HealthAssured.Checkout.Tests;

[TestFixture]
public class CheckoutTests
{
    private Checkout _checkout;

    [SetUp]
    public void Setup()
    {
        var pricingRules = new List<IPricingRule>
        {
            new SpecialOfferPricingRule("A", 3, 130, 50),
            new SpecialOfferPricingRule("B", 2, 45, 30),
            new StandardPricingRule("C", 20),
            new StandardPricingRule("D", 15)
        };

        var pricingEngine = new PricingEngine(pricingRules);
        _checkout = new Checkout(pricingEngine);
    }

    [Test]
    public void Scan_ShouldThrowException_ForUnknownSku()
    {
        Assert.Throws<ArgumentException>(() => _checkout.Scan("Z"), "Unknown SKU: Z");
    }

    [Test]
    public void GetTotalPrice_ShouldReturnCorrectPrice_WithoutSpecialOffers()
    {
        _checkout.Scan("C");
        _checkout.Scan("D");

        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(35));
    }

    [Test]
    public void GetTotalPrice_ShouldApplySpecialOffer_WhenConditionsMet()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");

        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(130));
    }

    [Test]
    public void GetTotalPrice_ShouldApplySpecialOffer_AndRegularPriceForRemainder()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");

        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(180)); // 130 (special offer) + 50 (single unit)
    }

    [Test]
    public void GetTotalPrice_ShouldHandleMultipleSpecialOffersAndSkus()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("C");

        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(195)); // 130 (A) + 45 (B) + 20 (C)
    }

    [Test]
    public void GetTotalPrice_ShouldReturnZero_WhenNoItemsScanned()
    {
        Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(0));
    }
}
