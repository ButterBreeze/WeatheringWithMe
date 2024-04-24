using System.Data.Entity.ModelConfiguration;

namespace Data.DatabaseObjects.Models.ModelMaps
{
    public class ApplicationSettingMap : EntityTypeConfiguration<ApplicationSetting>
    {
        public ApplicationSettingMap()
        {
            ToTable("APPLICATION_SETTINGS");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ID");
            Property(x => x.AppId).HasColumnName("APP_ID");
            Property(x => x.SettingName).HasColumnName("SETTING_NAME");
            Property(x => x.SettingValue).HasColumnName("SETTING_VALUE");
        }
    }
}