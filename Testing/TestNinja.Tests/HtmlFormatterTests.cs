using TestNinja.Fundamentals;

namespace TestNinja.Tests;

[TestFixture]
public class HtmlFormatterTests
{
    private HtmlFormatter _htmlFormatter=null!;
    
    [SetUp]
    public void Setup()
    {
        _htmlFormatter = new HtmlFormatter();
    }
    
    [Test]
    public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
    {
        var result = _htmlFormatter.FormatAsBold("string");
        
        Assert.That(result,Does.StartWith("<strong>").IgnoreCase);
        Assert.That(result,Does.EndWith("</strong>").IgnoreCase);
        Assert.That(result,Does.Contain("string").IgnoreCase);
    }
}