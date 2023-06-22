using TestNinja.Mocking;

namespace TestNinja.Tests.MockingTests;

[TestFixture]
public class ProductTests
{
    private Product _product = null!;
    
    [SetUp]
    public void Setup()
    {
        _product = new Product();
    }

    [Test]
    public void GetPrice_WhenCustomerIsGold_Discount70Percent()
    {
        _product.ListPrice = 10;
        
        var price = _product.GetPrice(new Customer { IsGold = true });
        
        Assert.That(price,Is.EqualTo(7));
    }

    [Test]
    public void Get_WhenCustomerIsCommon_ReturnListPrice()
    {
        _product.ListPrice = 100;

        var price = _product.GetPrice(new Customer());
        
        Assert.That(price,Is.EqualTo(100));
    }
}