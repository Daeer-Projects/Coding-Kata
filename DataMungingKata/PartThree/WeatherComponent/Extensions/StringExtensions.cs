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
                isWeatherValidType.ExtractWeatherItems(data);
            }
            else
            {
                isWeatherValidType.IsValid = false;
                isWeatherValidType.ErrorList.Add(errorMessage);
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

        private static void ExtractWeatherItems(this WeatherValidationType isWeatherValidType, IReadOnlyList<string> data)
        {
            var day = data[0];
            var maxTemp = data[1].RemoveAsterisk();
            var minTemp = data[2].RemoveAsterisk();

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

                isWeatherValidType.ProcessWeatherValidation(result);
            }
            else
            {
                isWeatherValidType.IsValid = false;
                isWeatherValidType.ErrorList.Add($"Invalid items. Day: {day}, Max Temp: {maxTemp}, Min Temp: {minTemp}.");
            }
        }

        private static string RemoveAsterisk(this string item)
        {
            return  item.Replace("*", string.Empty);
        }

        private static void ProcessWeatherValidation(this WeatherValidationType isWeatherValidType, Weather result)
        {
            var validationResult = result.IsValid();
            if (validationResult.IsValid)
            {
                isWeatherValidType.IsValid = true;
                isWeatherValidType.Weather = result;
            }
            else
            {
                isWeatherValidType.IsValid = false;
                isWeatherValidType.ErrorList.Add($"Invalid Weather.");

                foreach (var validationResultError in validationResult.Errors)
                {
                    isWeatherValidType.ErrorList.Add(validationResultError.ErrorMessage);
                }
            }
        }
    }
}
