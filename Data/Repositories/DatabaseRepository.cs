using System;
using System.Collections.Generic;
using System.Linq;
using Data.DatabaseObjects.Models;

namespace Data.Repositories
{
    public class DatabaseRepository : IDatabaseRepository, IDisposable
    {
        private const int WeatherWithMeAppId = 1;
        public DatabaseRepository(string connString)
        {
            _dbContext = new DatabaseContext(connString);
        }
        
        public List<ApplicationSetting> GetApplicationSettings()
        {
            return _dbContext.ApplicationSettings.Where(sett => sett.AppId == WeatherWithMeAppId).ToList();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
        
        private readonly DatabaseContext _dbContext;
    }
}