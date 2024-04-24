using System;
using System.Globalization;

namespace Data.MapObjects
{
    [Serializable]
    public class Coordinates
    {

        private const int ToDecimalPlace = 4;
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public string LatitudeRoundedToString => Math.Round(Latitude, ToDecimalPlace).ToString(CultureInfo.InvariantCulture);
        
        public string LongitudeRoundedToString => Math.Round(Longitude, ToDecimalPlace).ToString(CultureInfo.InvariantCulture);
        
    }
}