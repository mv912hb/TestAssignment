using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace TestAssignment.Resources;

public static class WikipediaApiHolder
{
    private static readonly HttpClient HttpClient = new();
    private const string ArticleName = "Test_automation";

    private static int GetSectionNumber()
    {
        var url = $"https://en.wikipedia.org/w/api.php?action=parse&page={ArticleName}&prop=sections&format=json";
        var response = HttpClient.GetStringAsync(url).Result;
        var jsonResponse = JObject.Parse(response);
        var sections = jsonResponse["parse"]!["sections"];

        var sectionNumber = -1;

        foreach (var section in sections!)
        {
            if (section["line"]!.ToString() != "Test-driven development") continue;
            sectionNumber = Convert.ToInt32(section["index"]!.ToString());
            break;
        }

        return sectionNumber;
    }

    private static string GetSectionText(int sectionNumber)
    {
        var url =
            $"https://en.wikipedia.org/w/api.php?action=parse&page=Test_automation&prop=text&section={sectionNumber}&format=json";
        var response = HttpClient.GetStringAsync(url).Result;
        var jsonResponse = JObject.Parse(response);
        var htmlText = jsonResponse["parse"]["text"]["*"].ToString();

        var textContent = Regex.Replace(htmlText, "<[^>]*>", "");

        textContent = Regex.Replace(textContent, @"\[.*?\]", "");

        var caretIndex = textContent.IndexOf('^');
        if (caretIndex != -1) textContent = textContent.Substring(0, caretIndex);

        return textContent.Trim();
    }


    private static Dictionary<string, int> ProcessTextAndCreateDictionary(string text)
    {
        var words = Regex.Replace(text.ToLower(), @"\[[^\]]*?\]|\d+|\W+", " ")
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        return words.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count());
    }

    public static Dictionary<string, int> GetDataFromApi()
    {
        var sectionNumber = GetSectionNumber();
        var textContent = GetSectionText(sectionNumber);
        return ProcessTextAndCreateDictionary(textContent);
    }
}