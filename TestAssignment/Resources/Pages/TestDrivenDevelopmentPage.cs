using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestAssignment.Resources.Pages;

public class TestDrivenDevelopmentPage
{
    private const string Url = "https://en.wikipedia.org/wiki/Test_automation#Test-driven_development";
    private static readonly By TDD_HEADER = By.Id("Test-driven_development");
    private static readonly By TDD_TEXT = By.XPath("./ancestor::h3/following-sibling::p[1]");

    private IWebDriver _driver;

    private TestDrivenDevelopmentPage()
    {
    }

    public static TestDrivenDevelopmentPage Instance { get; } = new();

    private void Initialize(IWebDriver driver)
    {
        _driver = driver;
    }

    public TestDrivenDevelopmentPage Navigate()
    {
        Initialize(SeleniumHolder.Instance.Driver);
        _driver.Navigate().GoToUrl(Url);
        return this;
    }

    private string GetPageText()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        return wait.Until(driver => driver.FindElement(TDD_HEADER)).Text + " " +
               wait.Until(driver => driver.FindElement(TDD_HEADER).FindElement(TDD_TEXT)).Text;
    }

    public Dictionary<string, int> GetDataFromPage()
    {
        var text = GetPageText();
        var processedText = ProcessText(text);
        return processedText;
    }

    private static Dictionary<string, int> ProcessText(string text)
    {
        var processedText = Regex.Replace(text.ToLower(), @"\[[^\]]*?\]|\d+|\W+", " ");
        var words = processedText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        return words.GroupBy(word => word)
            .ToDictionary(group => group.Key, group => group.Count());
        ;
    }
}