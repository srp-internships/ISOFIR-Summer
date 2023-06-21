using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.Tests;

[TestFixture]
public class MathTests
{
    private Math _math = null!;
    [SetUp]
    public void Init()
    {
        _math = new Math();
    }
        
    [Test]
    [TestCase(1,2)]
    [TestCase(4,8)]
    public void Add_WhenCall_ReturnSumOfArguments(int a,int b)
    {
        var result = _math.Add(a, b);
            
        Assert.That(result,Is.EqualTo(a+b));
    }

    [Test]
    [TestCase(1,5,5)]
    [TestCase(5,8,8)]
    [TestCase(10,8,10)]
    public void Max_WhenCall_ReturnMaxArgument(int a, int b, int exceptedResult)
    {
        var result = _math.Max(a, b);
            
        Assert.That(result,Is.EqualTo(exceptedResult));
    }

    [Test]
    [TestCase(1,new[]{1})]
    [TestCase(5, new[]{1,3,5})]
    [TestCase(9, new[]{1,3,5,7,9})]
    public void GetOddNumbers_LimitIsGreaterThenZero_ReturnOddNumbers(int limit, IEnumerable<int> exceptedVariantOfResult)
    {
        var list = _math.GetOddNumbers(limit);
            
        Assert.That(list, Is.EquivalentTo(exceptedVariantOfResult));
    }
}