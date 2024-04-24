using System.Collections.Generic;
using Data.MapObjects;
using Data.WeatherObjects;

namespace Data.Repositories
{
    public interface IForecastRepository
    {
        List<Period> GetHourlyWeatherPeriods(string coords);
        string GetHourlyForecastUri(Coordinates coords);
    }
}