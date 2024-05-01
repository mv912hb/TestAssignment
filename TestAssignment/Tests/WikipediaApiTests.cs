using NUnit.Framework;
using TestAssignment.Resources;
using TestAssignment.Resources.Pages;

namespace TestAssignment.Tests;

[TestFixture]
public class WikipediaApiTests : BaseClass
{
    [Test]
    public void CompareDataTest()
    {
        var dataFromApi = WikipediaApiHolder.GetDataFromApi();
        var dataFromUi = TestAutomationPage.Instance
            .Navigate()
            .GetDataFromPage();

        var notFoundInUi = dataFromApi
            .Where(item =>
                !dataFromUi.Keys.Any(key => string.Equals(key, item.Key, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        Assert.That(notFoundInUi, Is.Empty,
            $"Word occurrences from API call not found in UI parsing: {string.Join(Environment.NewLine, notFoundInUi)}");
    }
}