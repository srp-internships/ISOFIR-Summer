using TestNinja.Fundamentals;

namespace TestNinja.Tests.FundamentalsTests;

[TestFixture]
public class ErrorLoggerTests
{
    [SetUp]
    public void Setup()
    {
        _errorLogger = new ErrorLogger();
    }

    private ErrorLogger _errorLogger = null!;

    [Test]
    public void Log_WhenCalled_FillLastErrorProperty()
    {
        _errorLogger.Log("this is a last error");

        Assert.That(_errorLogger.LastError, Is.EqualTo("this is a last error"));
    }

    [Test]
    [TestCase("     ")]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Log_MessageIsNullOrWhiteSpace(string message)
    {
        Assert.That(() => _errorLogger.Log(message), Throws.ArgumentNullException);
    }

    [Test]
    public void Log_ValidMessage_InvokeErrorLoggedEvent()
    {
        var id = Guid.Empty;

        _errorLogger.ErrorLogged += (_, argId) => id = argId;
        _errorLogger.Log("the event test log");

        Assert.That(id, Is.Not.EqualTo(Guid.Empty));
    }
}