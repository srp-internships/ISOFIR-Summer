using TestNinja.Fundamentals;

namespace TestNinja.Tests;
[TestFixture]
public class DateHelperTests
{
    [Test]
    [TestCase(12,10,109)]
    [TestCase(12,10,1039)]
    [TestCase(12,10,1019)]
    [TestCase(12,10,99)]
    public void FirstOfNextMoth_IfMothEqual12_Return1JanNextYear(int moth, int day, int year)
    {
        var result = DateHelper.FirstOfNextMonth(new DateTime(year, moth, day));
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Month, Is.EqualTo(1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Year, Is.EqualTo(year + 1));
        });
    }

    [Test]
    [TestCase(10,10,109)]
    [TestCase(9,10,1039)]
    [TestCase(11,10,1019)]
    [TestCase(1,10,99)]
    public void FirstOfNextMoth_IfMothLessThan12_ReturnFirstDayOfNextMoth(int moth, int day, int year)
    {
        var result = DateHelper.FirstOfNextMonth(new DateTime(year, moth, day));
    
        Assert.Multiple(() =>
        {
            Assert.That(result.Month, Is.EqualTo(moth+1));
            Assert.That(result.Day, Is.EqualTo(1));
            Assert.That(result.Year, Is.EqualTo(year));
        });
    }
}