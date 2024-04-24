using System;
using System.Collections.Generic;
using System.Linq;
using Data.DatabaseObjects.Models;
using Data.Exceptions;
using Data.MapObjects;
using Data.Repositories;
using Data.WeatherObjects;
using log4net;

namespace BusinessLayer
{
    public class ProcessModel
    {
        private readonly ILog _log = LogManager.GetLogger("AdoNetAppenderException");
        
        public ProcessModel(IForecastRepository forecastRepo, IDatabaseRepository databaseRepo)
        {
            _databaseRepo = databaseRepo;
            _forecastRepo = forecastRepo;
        }
        public List<ApplicationSetting> GetApplicationSettings()
        {
            return  _databaseRepo.GetApplicationSettings();
        }
        
        public bool TryGetWeatherForCoordinates(Coordinates coords, out WeatherForecast weatherForecast)
        {
            try
            {
                var hourlyForecastUri = _forecastRepo.GetHourlyForecastUri(coords);
                if (hourlyForecastUri == string.Empty)
                {
                    throw new InvalidCoordinatesException($"Invalid Coordinates: {coords.LatitudeRoundedToString},{coords.LongitudeRoundedToString}");
                }
                var periods = _forecastRepo.GetHourlyWeatherPeriods(hourlyForecastUri);

                weatherForecast = GroupForecastDataForDisplay(periods);
            }
            catch (InvalidCoordinatesException ex)
            {
                // log the error
                _log.Error(ex.Message, ex);
                weatherForecast = null;
                return false;
            }

            return true;
        }
        
        private WeatherForecast GroupForecastDataForDisplay(List<Period> periods)
        {
            var weatherForecast = new WeatherForecast();
            var orderedPeriods = periods.OrderBy(p => p.Number).ToList();
            
            var firstPeriod = orderedPeriods.FirstOrDefault() ?? 
                              throw new Exception("No periods were found");
            
            weatherForecast.ForecastPeriods = orderedPeriods;
            weatherForecast.FirstPeriod = firstPeriod;

            var todaysPeriods = orderedPeriods.Where(op => op.StartTime.Date == DateTime.Today).ToList();

            weatherForecast.TodaysLowTemp = todaysPeriods.Min(tp => tp.Temperature);
            weatherForecast.TodaysHighTemp = todaysPeriods.Max(tp => tp.Temperature);
            
            return weatherForecast;
        }

        private readonly IForecastRepository _forecastRepo;
        private readonly IDatabaseRepository _databaseRepo;
    }
}