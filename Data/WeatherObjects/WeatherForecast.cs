using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Data.WeatherObjects
{
    [Serializable]
    public class WeatherForecast
    {
        [JsonProperty("periods")]
        public List<Period> ForecastPeriods { get; set; }
        
        public Period FirstPeriod { get; set; }
        
        public double TodaysLowTemp { get; set; }
        
        public double TodaysHighTemp { get; set; }

    }
}