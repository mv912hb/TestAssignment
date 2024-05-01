using System.Text.RegularExpressions;

namespace TestAssignment.Resources;

public static class CommonUtilities
{
    public static Dictionary<string, int> ProcessText(string text)
    {
        var processedText = Regex.Replace(text.ToLower(), @"\[[^\]]*?\]|\d+|\W+", " ");
        var words = processedText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        return words
            .GroupBy(word => word)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}