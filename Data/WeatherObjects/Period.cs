using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Data.WeatherObjects
{
    [Serializable]
    public class Period
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        public string DisplayStartTime => StartTime.ToString("h tt", CultureInfo.InvariantCulture);

        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

        [JsonProperty("isDaytime")]
        public bool IsDaytime { get; set; }

        [JsonProperty("temperatureTrend")]
        public string TemperatureTrend { get; set; }
        
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        
        [JsonProperty("temperatureUnit")]
        public string TemperatureUnit { get; set; }


        [JsonProperty("probabilityOfPrecipitation")]
        public ProbabilityOfPrecipitation ProbabilityOfPrecipitation { get; set; }

        [JsonProperty("dewpoint")]
        public Dewpoint Dewpoint { get; set; }

        [JsonProperty("relativeHumidity")]
        public RelativeHumidity RelativeHumidity { get; set; }

        [JsonProperty("windDirection")]
        public string WindDirection { get; set; }
        
        [Obsolete("Api will return this, but will remove this in the near future")]
        [JsonProperty("windSpeed")]
        public string WindSpeed { get; set; }
        
        [JsonProperty("shortForecast")]
        public string ShortForecast { get; set; }

        [JsonProperty("detailedForecast")]
        public string DetailedForecast { get; set; }
    }
}