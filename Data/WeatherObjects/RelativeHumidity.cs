using Newtonsoft.Json;

namespace Data.WeatherObjects
{
    public class RelativeHumidity
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("maxValue")]
        public int MaxValue { get; set; }

        [JsonProperty("minValue")]
        public int MinValue { get; set; }

        [JsonProperty("unitCode")]
        public string UnitCode { get; set; }

        [JsonProperty("qualityControl")]
        public string QualityControl { get; set; }
    }
}