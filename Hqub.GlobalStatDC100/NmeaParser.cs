using System;
using System.Globalization;

namespace Hqub.GlobalSat
{
    public class NmeaParser
    {
        public NmeaParser()
        {
            
        }

        public static GpsPoint Parse(string sentence)
        {
            if (IsValid(sentence))
            {
                switch (GetWords(sentence)[0])
                {
                    case "$GPRMC":
                        return ParseGPRMC(sentence);

                    case "$GPGGA":
                        return ParseGPGGA(sentence);
                }
            }
            return null;
        }

        private static GpsPoint ParseGPGGA(string sentence)
        {
            var interest = new GpsPoint();
//            interest.TypePoi = 0;
            string[] words = GetWords(sentence);
            if ((((words[2] != "") & (words[3] != "")) & (words[4] != "")) & (words[5] != ""))
            {
                int degrees = Convert.ToInt32(words[2].Substring(0, 2));
                double minutes = Convert.ToDouble(words[2].Substring(2), CultureInfo.InvariantCulture);
                interest.Latitude = DMSToDecimalDegrees(degrees, minutes, 0.0);
                if (words[3].ToUpper() == "S")
                {
                    interest.Latitude *= -1.0;
                }
                degrees = Convert.ToInt32(words[4].Substring(0, 3));
                minutes = Convert.ToDouble(words[4].Substring(3), CultureInfo.InvariantCulture);
                interest.Longitude = DMSToDecimalDegrees(degrees, minutes, 0.0);
                if (words[5].ToUpper() == "W")
                {
                    interest.Longitude *= -1.0;
                }
            }
            if (words[1] != "")
            {
                int hour = Convert.ToInt32(words[1].Substring(0, 2));
                int minute = Convert.ToInt32(words[1].Substring(2, 2));
                int second = Convert.ToInt32(words[1].Substring(4, 2));
                int millisecond = 0;
                if (words[1].Length > 7)
                {
                    millisecond = Convert.ToInt32(words[1].Substring(7));
                }
                DateTime time2 = DateTime.Today.ToUniversalTime();
                DateTime time3 = new DateTime(time2.Year, time2.Month, time2.Day, hour, minute, second, millisecond);
                interest.Time = time3;
//                interest.TypePoi = 1;
            }
//            interest.Speed = new SpeedMeasurement();
//            interest.Altitude = new ElevationMeasurement();
//            if (words[9] != "")
//            {
//                interest.Altitude.SetValue(double.Parse(words[9], CultureInfo.InvariantCulture), MeasurementSystem.Metric);
//                interest.TypePoi = 2;
//            }
            return interest;
        }


        private static GpsPoint ParseGPRMC(string sentence)
        {
            var interest = new GpsPoint();
//            interest.TypePoi = 0;
            string[] words = GetWords(sentence);
            if ((((words[3] != "") & (words[4] != "")) & (words[5] != "")) & (words[6] != ""))
            {
                int degrees = Convert.ToInt32(words[3].Substring(0, 2));
                double minutes = Convert.ToDouble(words[3].Substring(2), CultureInfo.InvariantCulture);
                interest.Latitude = DMSToDecimalDegrees(degrees, minutes, 0.0);
                if (words[4].ToUpper() == "S")
                {
                    interest.Latitude *= -1.0;
                }
                degrees = Convert.ToInt32(words[5].Substring(0, 3));
                minutes = Convert.ToDouble(words[5].Substring(3), CultureInfo.InvariantCulture);
                interest.Longitude = DMSToDecimalDegrees(degrees, minutes, 0.0);
                if (words[6].ToUpper() == "W")
                {
                    interest.Longitude *= -1.0;
                }
            }
            if ((words[1] != "") && !string.IsNullOrEmpty(words[9]))
            {
                int hour = Convert.ToInt32(words[1].Substring(0, 2));
                int minute = Convert.ToInt32(words[1].Substring(2, 2));
                int second = Convert.ToInt32(words[1].Substring(4, 2));
                int millisecond = 0;
                if (words[1].Length > 7)
                {
                    millisecond = Convert.ToInt32(words[1].Substring(7));
                }
                int day = Convert.ToInt32(words[9].Substring(0, 2));
                int month = Convert.ToInt32(words[9].Substring(2, 2));
                int year = (Convert.ToInt32(DateTime.Today.Year.ToString().Substring(0, 2)) * 100) + Convert.ToInt32(words[9].Substring(4, 2));
                DateTime time2 = new DateTime(year, month, day, hour, minute, second, millisecond);
                interest.Time = time2;
//                interest.TypePoi = 1;
            }

//            interest.Speed = new SpeedMeasurement();
//            if (words[7] != "")
//            {
//                interest.Speed.SetValue(double.Parse(words[7], CultureInfo.InvariantCulture), MeasurementSystem.Nautical);
//            }
//            if (words[8] != "")
//            {
//                double.Parse(words[8], CultureInfo.InvariantCulture);
//            }
//            interest.Altitude = new ElevationMeasurement();
            return interest;
        }

        private static string GetChecksum(string sentence)
        {
            var num = 0;
            foreach (var ch in sentence)
            {
                if (ch != '$')
                {
                    if (ch == '*')
                    {
                        break;
                    }
                    if (num == 0)
                    {
                        num = Convert.ToByte(ch);
                    }
                    else
                    {
                        num ^= Convert.ToByte(ch);
                    }
                }
            }
            return num.ToString("X2");
        }

        private static string[] GetWords(string sentence)
        {
            return sentence.Split(new char[] { ',' });
        }

        private static bool IsValid(string sentence)
        {
            return (sentence.Substring(sentence.IndexOf("*") + 1) == GetChecksum(sentence));
        }

        public static double DMSToDecimalDegrees(int degrees, double minutes, double seconds)
        {
            return ((degrees + (minutes / 60.0)) + (seconds / 3600.0));
        }
    }
}
