using System.Collections.Generic;

namespace WeatherComponent.Types
{
    public class WeatherValidationType
    {
        public Weather Weather { get; set; }
        public bool IsValid { get; set; }
        public List<string> ErrorList { get; set; }

        public WeatherValidationType()
        {
            Weather = new Weather();
            IsValid = false;
            ErrorList = new List<string>();
        }
    }
}
