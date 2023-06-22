using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.MockingTests;

[TestFixture]
public class OrderServiceTests
{
    private OrderService _orderService=null!;
    private Mock<IStorage> _storageMock=null!;
    
    [SetUp]
    public void Setup()
    {
        _storageMock = new Mock<IStorage>();
        _orderService = new OrderService(_storageMock.Object);
    }

    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var order = new Order();

        _orderService.PlaceOrder(order);
        
        _storageMock.Verify(s=>s.Store(order));
    }
}