using TestNinja.Fundamentals;

namespace TestNinja.Tests.FundamentalsTests;

[TestFixture]
public class PhoneNumberTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Parse_ArgIsNullOrWriteSpace_ThrowArgumentException(string arg)
    {
        Assert.That(() => PhoneNumber.Parse(arg), Throws.ArgumentException);
    }

    [Test]
    public void Parse_ArgNotEqual10_ThrowArgumentException()
    {
        Assert.Multiple(() =>
        {
            Assert.That(() => PhoneNumber.Parse("11"), Throws.ArgumentException);
            Assert.That(() => PhoneNumber.Parse("9"), Throws.ArgumentException);
        });
    }

    [Test]
    public void Parse_ArgIsValid_ReturnPhoneNumber()
    {
        var result = PhoneNumber.Parse("1234567890");
        Assert.Multiple(() =>
        {
            Assert.That(result.Area, Is.EqualTo("123"));
            Assert.That(result.Major, Is.EqualTo("456"));
            Assert.That(result.Minor, Is.EqualTo("7890"));
        });
    }

    [Test]
    public void ToString_WhenCall_ReturnAreaMajorMinorFormat()
    {
        var phoneNumber = PhoneNumber.Parse("1234567890");

        var result = phoneNumber.ToString();

        Assert.That(result, Is.EqualTo("(123)456-7890"));
    }
}