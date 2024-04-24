namespace Data.DatabaseObjects.Models
{
    public class ApplicationSetting
    {
        
        public int Id { get; set; }
        public int AppId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue{ get; set; }
    }
}