using System.Collections.Generic;
using Data.DatabaseObjects.Models;

namespace Data.Repositories
{
    public interface IDatabaseRepository
    {
        List<ApplicationSetting> GetApplicationSettings();
    }
}