using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.Tests;

[TestFixture]
public class ReservationTests
{
    private Reservation _reservation=null!;
    private User _user=null!;
    [SetUp]
    public void SetUp()
    {
        _user = new User();
        _reservation = new Reservation();
    }
    
    [Test]
    public void CanBeCancelledBy_AdminCancelling_ReturnTrue()
    {
        _user.IsAdmin = true;
        
        var result = _reservation.CanBeCancelledBy(_user);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void CanBeCancelledBy_SameUserCancelling_ReturnTrue()
    {
        _reservation.MadeBy = _user;
        
        var result = _reservation.CanBeCancelledBy(_user);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void CanBeCancelledBy_AnotherUserCancelling_ReturnTrue()
    {
        var result = _reservation.CanBeCancelledBy(_user);
        
        Assert.That(result, Is.False);
    }
}