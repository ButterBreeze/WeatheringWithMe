using Newtonsoft.Json;

namespace Data.WeatherObjects
{
    public class Dewpoint
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("maxValue")]
        public double MaxValue { get; set; }

        [JsonProperty("minValue")]
        public double MinValue { get; set; }

        [JsonProperty("unitCode")]
        public string UnitCode { get; set; }

        [JsonProperty("qualityControl")]
        public string QualityControl { get; set; }
    }
}