using System;

namespace Hqub.GlobalSat
{
    public class GpsPoint
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Time { get; set; }

        public double Speed { get; set; }

        public double Distance { get; set; }
    }
}
