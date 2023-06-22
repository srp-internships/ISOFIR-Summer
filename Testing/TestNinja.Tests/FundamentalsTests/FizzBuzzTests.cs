using TestNinja.Fundamentals;

namespace TestNinja.Tests.FundamentalsTests;

[TestFixture]
public class FizzBuzzTests
{
    [Test]
    [TestCase(3)]
    [TestCase(33)]
    [TestCase(333)]
    public void GetOutput_InputIsDivisibleOnly3(int input)
    {
        var result = FizzBuzz.GetOutput(input);

        Assert.That(result, Is.EqualTo("Fizz"));
    }

    [Test]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(110)]
    public void GetOutput_InputIsDivisibleOnly5(int input)
    {
        var result = FizzBuzz.GetOutput(input);

        Assert.That(result, Is.EqualTo("Buzz"));
    }

    [Test]
    [TestCase(15)]
    public void GetOutput_InputIsDivisible5And3(int input)
    {
        var result = FizzBuzz.GetOutput(input);

        Assert.That(result, Is.EqualTo("FizzBuzz"));
    }

    [Test]
    [TestCase(1)]
    public void GetOutput_InputIsDivisible5Or3_ReturnTheSameNumber(int input)
    {
        var result = FizzBuzz.GetOutput(input);

        Assert.That(result, Is.EqualTo(input.ToString()));
    }
}