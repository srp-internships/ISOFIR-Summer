using TestNinja.Fundamentals;

namespace TestNinja.Tests;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _customerController=null!;
    [SetUp]
    public void Setup()
    {
        _customerController = new CustomerController();
    }
    [Test]
    public void GetCustomer_WhenIdEqual0_ReturnNotFoundResult()
    {
        var result = _customerController.GetCustomer(0);
        
        Assert.That(result, Is.TypeOf<NotFound>());
    }

    [Test]
    public void GetCustomer_IdIsGreaterThan0_ReturnOkResult()
    {
        var result = _customerController.GetCustomer(62);
        
        Assert.That(result,Is.TypeOf<Ok>());
    }
}