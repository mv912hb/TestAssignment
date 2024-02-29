using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestAssignment.Resources;

public class SeleniumHolder
{
    private static SeleniumHolder _instance;
    private static readonly object Padlock = new();
    private IWebDriver _driver;

    private SeleniumHolder()
    {
    }

    public IWebDriver Driver
    {
        get
        {
            if (_driver is null) OpenDriver();
            return _driver;
        }
    }

    public static SeleniumHolder Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new SeleniumHolder();
            }
        }
    }

    public void OpenDriver()
    {
        _driver ??= new ChromeDriver();
    }

    public void CloseDriver()
    {
        _driver?.Quit();
        _driver = null;
    }
}