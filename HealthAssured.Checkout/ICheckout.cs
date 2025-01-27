namespace HealthAssured.Checkout;

public interface ICheckout
{
    void Scan(string item);

    int GetTotalPrice();
}
