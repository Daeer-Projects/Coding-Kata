using System.Collections.Generic;

namespace WeatherComponentV2.Types
{
    public class WeatherValidationType
    {
        public Weather Weather { get; set; }
        public bool IsValid { get; set; }
        public List<string> ErrorList { get; }

        public WeatherValidationType()
        {
            Weather = new Weather();
            IsValid = false;
            ErrorList = new List<string>();
        }
    }
}
