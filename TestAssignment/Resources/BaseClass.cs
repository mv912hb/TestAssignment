using NUnit.Framework;

namespace TestAssignment.Resources;

public class BaseClass
{
    [SetUp]
    public void SetUp()
    {
        SeleniumHolder.Instance.OpenDriver();
    }

    [TearDown]
    public void TearDown()
    {
        SeleniumHolder.Instance.CloseDriver();
    }
}