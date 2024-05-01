using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace TestAssignment.Resources;

public static class WikipediaApiHolder
{
    private const string ArticleName = "Test_automation";
    private const string SectionName = "Test-driven development";
    private const string BaseUrl = "https://en.wikipedia.org/w/api.php?action=parse&page";
    private static readonly HttpClient HttpClient = new();

    private static int GetSectionNumber()
    {
        var url = $"{BaseUrl}={ArticleName}&prop=sections&format=json";
        var response = HttpClient.GetStringAsync(url).Result;
        var jsonResponse = JObject.Parse(response);
        var sections = jsonResponse["parse"]!["sections"];

        var sectionNumber = -1;

        foreach (var section in sections!)
        {
            if (section["line"]!.ToString() is not SectionName) continue;
            sectionNumber = Convert.ToInt32(section["index"]!.ToString());
            break;
        }

        return sectionNumber;
    }

    private static string GetSectionText(int sectionNumber)
    {
        var url =
            $"{BaseUrl}={ArticleName}&prop=text&section={sectionNumber}&format=json";
        var response = HttpClient.GetStringAsync(url).Result;
        var jsonResponse = JObject.Parse(response);
        var htmlText = jsonResponse["parse"]!["text"]!["*"]!.ToString();

        var textContent = Regex.Replace(htmlText, "<[^>]*>", "");

        textContent = Regex.Replace(textContent, @"\[.*?\]", "");

        var caretIndex = textContent.IndexOf('^');
        if (caretIndex != -1) textContent = textContent.Substring(0, caretIndex);

        return textContent.Trim();
    }

    public static Dictionary<string, int> GetDataFromApi()
    {
        var sectionNumber = GetSectionNumber();
        var textContent = GetSectionText(sectionNumber);
        return CommonUtilities.ProcessText(textContent);
    }
}