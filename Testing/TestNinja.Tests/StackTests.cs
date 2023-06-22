using TestNinja.Fundamentals;

namespace TestNinja.Tests;

[TestFixture]
public class StackTests
{
    private Fundamentals.Stack<Reservation> _stack=null!;

    [SetUp]
    public void Setup()
    {
        _stack = new Fundamentals.Stack<Reservation>();
    }
    
    [Test]
    public void Push_ValidArg_AddObjectToList()
    {
        _stack.Push(new Reservation());
        
        Assert.That(_stack.Count,Is.EqualTo(1));
    }

    [Test]
    public void Push_ArgIsNull_ThrowArgIsNullException()
    {
        Assert.That(()=>{_stack.Push(null);},Throws.ArgumentNullException);
    }

    [Test]
    public void Pop_WhenStackIsEmpty_ThrowInvalidOperationException()
    {
        Assert.That(() => { _stack.Pop();},Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_WhenStackIsNotEmpty_ReturnLastObjectAndRemoveThat()
    {
        var last = new Reservation();
        
        _stack.Push(new Reservation());
        _stack.Push(new Reservation());
        _stack.Push(last);

        var result = _stack.Pop();
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(last));
            Assert.That(_stack.Count, Is.EqualTo(2));
        });
    }
}