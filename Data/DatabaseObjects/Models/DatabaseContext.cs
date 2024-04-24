using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using Data.DatabaseObjects.Models.ModelMaps;

namespace Data.DatabaseObjects.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<DatabaseContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ApplicationSettingMap());
        }
        
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

    }
}