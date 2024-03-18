using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookReceiver.Tests
{
    public class UtilityServiceTests
    {
        public string GetUnformatedDateTimeString()
        {
            return "20240315T154352-0400";
        }
        public string GetFormattedDateTimeString()
        {
            return "2024-03-15T15:43:52-04:00";
        }
        public string GetBadDateString()
        {
            return "240315T154352-0400";
        }
        public string GetHyperLinkFormat()
        {
            return "[Foo](Bar)";
        }
        [Fact]
        public void FormatDateTimeString_Does_Create_RFC_3389_Format()
        {
            //arrange
            string timeString = GetUnformatedDateTimeString();
            string expected = GetFormattedDateTimeString();
            //act
            string result = UtilityService.FormatDateTimeString(timeString);
            //assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void FormatDateTimeString_Does_Return_StringEmpty_When_Given_Invalid_Input()
        {
            //arrange
            string badDateString = GetBadDateString();
            string expected = string.Empty;
            //act
            string result = UtilityService.FormatDateTimeString(badDateString);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FormatAdaptiveCardHyperLink_Does_Return_Hyperlink_Format()
        {
            //arrange
            string text = "Foo";
            string hyperlink = "Bar";
            string expected = GetHyperLinkFormat();
            //act
            string result = UtilityService.FormatAdaptiveCardHyperLink(text, hyperlink);
            //assert
            Assert.Equal(expected, result);
        }
    }
}
