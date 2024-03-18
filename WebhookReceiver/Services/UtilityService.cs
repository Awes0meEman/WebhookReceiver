using System.Text.RegularExpressions;

namespace WebhookReceiver.Services
{
    public static class UtilityService
    {
        //formats a datetime string to match RFC 3389 format
        public static string FormatDateTimeString(string input)
        {
            if(!Regex.Match(input, "[0-9]{8}T[0-9]{6}-[0-9]{4}").Success)
            {
                //incorrect format
                return string.Empty;
            }
            string output = string.Empty;
            output = input.Split("T")[0].Insert(4, "-").Insert(7,"-");
            output += "T";
            output += input.Split("T")[1].Insert(2,":").Insert(5,":").Insert(11,":");
            return output;
        }

        public static string FormatAdaptiveCardHyperLink(string text, string hyperlink)
        {
            return $"[{text}]({hyperlink})";
        }
    }
}
