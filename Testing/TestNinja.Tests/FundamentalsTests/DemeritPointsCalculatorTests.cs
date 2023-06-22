using TestNinja.Fundamentals;

namespace TestNinja.Tests.FundamentalsTests;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    [SetUp]
    public void Setup()
    {
        _demeritPointsCalculator = new DemeritPointsCalculator();
    }

    private DemeritPointsCalculator _demeritPointsCalculator = null!;

    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(10, 0)]
    [TestCase(64, 0)]
    [TestCase(100, 7)]
    public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoint(int speed, int exceptedResult)
    {
        var result = _demeritPointsCalculator.CalculateDemeritPoints(speed);

        Assert.That(result, Is.EqualTo(exceptedResult));
    }
}