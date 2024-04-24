using Newtonsoft.Json;

namespace Data.WeatherObjects
{
    public class ProbabilityOfPrecipitation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

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