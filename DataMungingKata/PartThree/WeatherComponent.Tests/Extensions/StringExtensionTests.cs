using FluentAssertions;
using WeatherComponent.Extensions;
using Xunit;

namespace WeatherComponent.Tests.Extensions
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData(" hello")]
        [InlineData(" goodbye")]
        [InlineData("88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5")]
        [InlineData("235385283452908457290734975092837409273498572098754092743975098237450927349520670216387109273049")]
        [InlineData("  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP")]
        [InlineData("  mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3")]
        public void Test_to_weather_with_invalid_string_sets_is_valid_to_false(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToWeather();

            // Assert.
            result.IsValid.Should().BeFalse("the data file is not convertible to a weather instance.");
        }

        [Theory]
        [InlineData(" hello")]
        [InlineData(" goodbye")]
        [InlineData(" ")]
        public void Test_to_weather_with_short_invalid_string_sets_error_list(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToWeather();

            // Assert.
            result.ErrorList.Should().Contain(x => x.Contains("Exception raised:"));
        }

        [Theory]
        [InlineData(" hello to who ever gets to read this!  I hope it is OK for you! Enjoy! :)")]
        [InlineData(" goodbye to the same person who is reading this.  I hope you have fun! :)")]
        [InlineData("  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP")]
        public void Test_to_weather_with_long_invalid_string_sets_error_list(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToWeather();

            // Assert.
            result.ErrorList.Should().Contain(x => x.Contains("Invalid items."));
        }

        [Theory]
        [InlineData("   0  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5")]
        [InlineData("-120  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5")]
        [InlineData("  32  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5")]
        [InlineData("  15  88.4  88.5  74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5")]
        public void Test_to_weather_with_valid_format_but_invalid_items_sets_error_list(string inputString)
        {
            // Arrange.
            // Act.
            var result = inputString.ToWeather();

            // Assert.
            result.ErrorList.Should().Contain(x => x.Contains("Invalid Weather."));
        }
    }
}
