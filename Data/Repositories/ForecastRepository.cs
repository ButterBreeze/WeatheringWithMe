using System.Collections.Generic;
using System.Net.Http;
using Data.Exceptions;
using Data.MapObjects;
using Data.WeatherObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private const string ApiInvalidCoordsType = "https://api.weather.gov/problems/InvalidPoint";
        private const string ApiMarineForecastNotSupported = "https://api.weather.gov/problems/MarineForecastNotSupported";
        public ForecastRepository(string apiUri)
        {
            this._apiUri = apiUri;
        }

        public List<Period> GetHourlyWeatherPeriods(string hourlyForecastUri)
        {
            
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
                var response = client.GetAsync(hourlyForecastUri).Result;

                var parsedObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (!response.IsSuccessStatusCode && parsedObject["type"]?.ToString() == ApiMarineForecastNotSupported)
                {
                    throw new InvalidCoordinatesException(parsedObject["message"]?.ToString());
                }

                response.EnsureSuccessStatusCode();
                
                var periods = JsonConvert.DeserializeObject<List<Period>>(parsedObject["properties"]["periods"].ToString());
                return periods;
            }
        }
        
        public string GetHourlyForecastUri(Coordinates coords)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
                var response = client.GetAsync($"{_apiUri}/points/{coords.LatitudeRoundedToString},{coords.LongitudeRoundedToString}").Result;
                
                // check if status code is successful after checking for the invalid coordinates error
                var parsedObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (!response.IsSuccessStatusCode && parsedObject["type"]?.ToString() == ApiInvalidCoordsType)
                {
                    throw new InvalidCoordinatesException($"Invalid Coordinates: {coords.LatitudeRoundedToString},{coords.LongitudeRoundedToString}");
                }

                response.EnsureSuccessStatusCode();
                
                return parsedObject["properties"]["forecastHourly"].ToString();
            }
        }

        private readonly string _apiUri;
    }
}