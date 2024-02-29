using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
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
        var dataFromUi = TestDrivenDevelopmentPage.Instance
            .Navigate()
            .ProcessAndPrintText(); 
        
        var notFoundInApi = dataFromApi
            .Where(item => !dataFromUi.Keys.Any(key => string.Equals(key, item.Key, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        Assert.That(notFoundInApi, Is.Empty,
            $"Word occurrences from API call not found in UI parsing: {string.Join(Environment.NewLine, notFoundInApi)}");
    }
}