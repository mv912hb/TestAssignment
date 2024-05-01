using NUnit.Framework;

namespace TestAssignment.Resources;

public class BaseClass
{
    [SetUp]
    public void SetUp()
    {
        SeleniumHolder.Instance.OpenBrowser();
    }

    [TearDown]
    public void TearDown()
    {
        SeleniumHolder.Instance.CloseBrowser();
    }
}