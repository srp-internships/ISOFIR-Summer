using TestNinja.Fundamentals;

namespace TestNinja.Tests.FundamentalsTests;

[TestFixture]
public class HtmlFormatterTests
{
    [SetUp]
    public void Setup()
    {
        _htmlFormatter = new HtmlFormatter();
    }

    private HtmlFormatter _htmlFormatter = null!;

    [Test]
    public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
    {
        var result = _htmlFormatter.FormatAsBold("string");

        Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
        Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
        Assert.That(result, Does.Contain("string").IgnoreCase);
    }
}