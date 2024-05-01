using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestAssignment.Resources.Pages;

public class TestAutomationPage
{
    private const string Url = "https://en.wikipedia.org/wiki/Test_automation#Test-driven_development";
    private static readonly By TddHeader = By.Id("Test-driven_development");
    private static readonly By TddText = By.XPath("./ancestor::h3/following-sibling::p[1]");

    private IWebDriver? _driver;

    public static TestAutomationPage Instance { get; } = new();

    private void Initialize(IWebDriver? driver)
    {
        _driver = driver;
    }

    public TestAutomationPage Navigate()
    {
        Initialize(SeleniumHolder.Instance.Driver);
        _driver!.Navigate().GoToUrl(Url);
        return this;
    }

    private string GetPageText()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        return wait.Until(driver => driver.FindElement(TddHeader)).Text + " " +
               wait.Until(driver => driver.FindElement(TddHeader).FindElement(TddText)).Text;
    }

    public Dictionary<string, int> GetDataFromPage() => CommonUtilities.ProcessText(GetPageText());
}