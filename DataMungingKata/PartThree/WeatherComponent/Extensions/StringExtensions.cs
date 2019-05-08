using System;
using System.Collections.Generic;

using WeatherComponent.Configuration;
using WeatherComponent.Types;

namespace WeatherComponent.Extensions
{
    public static class StringExtensions
    {
        public static WeatherValidationType ToWeather(this string item)
        {
            var isWeatherValidType = new WeatherValidationType();

            var (canExtract, errorMessage, data) = CanExtractWeatherItems(item);
            if (canExtract)
            {
                isWeatherValidType = ExtractWeatherItems(data, isWeatherValidType);
            }
            else
            {
                isWeatherValidType.IsValid = false;
                isWeatherValidType.ErrorList.Add(errorMessage);
            }

            return isWeatherValidType;
        }

        private static WeatherValidationType ExtractWeatherItems(List<string> data, WeatherValidationType isWeatherValidType)
        {
            var day = data[0];
            var maxTemp = data[1];
            var minTemp = data[2];

            if (int.TryParse(day, out var dayAsInt) &&
                float.TryParse(maxTemp, out var maxTempAsFloat) &&
                float.TryParse(minTemp, out var minTempAsFloat))
            {
                var result = new Weather
                {
                    Day = dayAsInt,
                    MaximumTemperature = maxTempAsFloat,
                    MinimumTemperature = minTempAsFloat
                };

                if (result.IsValid())
                {
                    isWeatherValidType.IsValid = true;
                    isWeatherValidType.Weather = result;
                }
                else
                {
                    isWeatherValidType.IsValid = false;
                    isWeatherValidType.ErrorList.Add($"Invalid Weather.");
                }
            }
            else
            {
                isWeatherValidType.IsValid = false;
                isWeatherValidType.ErrorList.Add($"Invalid items. Day: {day}, Max Temp: {maxTemp}, Min Temp: {minTemp}.");
            }

            return isWeatherValidType;
        }

        private static (bool, string, List<string>) CanExtractWeatherItems(string item)
        {
            var results = (canExtract: false, errorMessage: string.Empty, dataList: new List<string>());

            try
            {
                results.dataList.Add(item.Substring(WeatherConfig.DayColumnStart, WeatherConfig.DayColumnLength));
                results.dataList.Add(item.Substring(WeatherConfig.MaxTempColumnStart, WeatherConfig.MaxTempColumnLength));
                results.dataList.Add(item.Substring(WeatherConfig.MinTempColumnStart, WeatherConfig.MinTempColumnLength));
                results.canExtract = true;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                results.canExtract = false;
                results.errorMessage = $"Exception raised: {exception.Message}.";
            }

            return results;
        }
    }
}
