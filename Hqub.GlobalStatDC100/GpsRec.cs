using System;
using System.IO;
using System.Text;

namespace Hqub.GlobalSat
{
    /// <summary>
    /// author: Glebov Boris
    /// author java version: Stefan Kaintoch
    /// </summary>
    public class GpsRec
    {
        //  aa aa aa aa: latitude: 
        //  bb bb bb bb: longitude: 
        //  cc cc cc cc: zulu-time: hh * 10000 + mm * 100 + ss
        //  dd dd dd dd: date: DD * 10000 + MM * 100 + (YY - 2000)
        //  ee ee ee ee: speed (km/h): speed * 100
        //  ff ff ff ff: altitude (m): alt * 10000

        private int latitude = -1;
        private int longitude = -1;
        private int timeZ = -1;
        private int date = -1;
        private int speed;
        private int altitude;
        private int unk1 ;
        private int typeOfCurRec = -1;
        private int typeOfNextRec = -1;

        public GpsRec()
        {
            typeOfNextRec = 2;
            typeOfCurRec = 2;
            latitude = 0;
            longitude = 0;
            timeZ = 0;
            date = 0;
            speed = 0;
            altitude = 0;
            unk1 = -1;
        }

        public void copy(GpsRec init)
        {
            typeOfNextRec = init.typeOfNextRec;
            typeOfCurRec = init.typeOfCurRec;
            latitude = init.latitude;
            longitude = init.longitude;
            timeZ = init.timeZ;
            date = init.date;
            speed = init.speed;
            altitude = init.altitude;
            unk1 = init.unk1;
        }

        public GpsRec(GpsRec init)
        {
            copy(init);
        }

        public bool equals(Object arg0)
        {
            var isEqual = false;
            if (arg0 != null && arg0 is GpsRec)
            {
                var otherGpsRec = (GpsRec) arg0;
                isEqual =
                    typeOfNextRec == otherGpsRec.typeOfNextRec
                    && typeOfCurRec == otherGpsRec.typeOfCurRec
                    && latitude == otherGpsRec.latitude
                    && longitude == otherGpsRec.longitude
                    && timeZ == otherGpsRec.timeZ
                    && date == otherGpsRec.date
                    && speed == otherGpsRec.speed
                    && altitude == otherGpsRec.altitude
                    && unk1 == otherGpsRec.unk1
                    ;
            }
            return isEqual;
        }

        public GpsRec(BinaryReader buf, int recType)
        {
            typeOfNextRec = recType;
            typeOfCurRec = recType;

            switch (recType)
            {
                case 0:
                    latitude = GetInt(buf);
                    longitude = GetInt(buf);
                    break;
                case 1:
                    date = GetInt(buf);
                    //Это условие для моего девайся, так как по какой то причине в режиме 1 , данные записываются по разному,
                    //либо сначала идет дата, либо широта
                    if (date.ToString().Length > 6)
                    {
                        latitude = date;
                        longitude = GetInt(buf);

                        timeZ = GetInt(buf);
                        date = GetInt(buf);
                        speed = GetInt(buf);
                    }
                    else
                    {
                        speed = GetInt(buf);

                        latitude = GetInt(buf);
                        longitude = GetInt(buf);

                        timeZ = GetInt(buf);
                    }
                    

                    break;

                case 2:
                    latitude = GetInt(buf);
                    longitude = GetInt(buf);

                    timeZ = GetInt(buf);
                    date = GetInt(buf);
                    speed = GetInt(buf);

                    altitude = GetInt(buf);
                    unk1 = GetInt(buf);
                    typeOfNextRec = GetInt(buf);
                    break;
            }
        }

        /**
         * Shows wether this is a valid GPS record.
         * @return true if GPS record is valid; otherwise false.
         */
        public bool isValid()
        {
            return
                latitude <= 360000000
                && latitude >= 0
                && longitude <= 360000000
                && longitude >= 0
                && timeZ >= 0
                && timeZ <= 240000
                && unk1 >= 0
                && unk1 <= 1
                ;
        }

        public int getTypeOfNextRec()
        {
            return typeOfNextRec;
        }

        public override String ToString()
        {
            return "[GpsRec: "
                + " lat = " + latitude
                + ", long = " + longitude
                + ", timeZ = " + timeZ
                + ", date = " + date
                + ", speed = " + speed
                + ", alt = " + altitude
                + ", unk1 = " + unk1
                + ", typeOfCurRec = " + typeOfCurRec
                + ", typeOfNextRec = " + typeOfNextRec
                + "]";
        }

        /**
         * @return Returns the latitude.
         */
        public int getLatitude()
        {
            return latitude;
        }

        /**
         * Converts this object to its GPX representation. 
         * @return this object's GPX representation as a String.
         */
        public String ToGpxTrkpt()
        {
            //		<trkpt lat="47.6972383333" lon="11.4178650000">
            //		<ele>662.0000000000</ele>
            //		<time>2007-04-21T13:56:05Z</time>
            //		<speed>1.0833333333</speed>
            //		</trkpt>
            var buf = new StringBuilder();
            buf.Append("<trkpt");
            buf.Append(" lat=\"").Append(ToDegree(latitude).ToString().Replace(',', '.')).Append("\"");
            buf.Append(" lon=\"").Append(ToDegree(longitude).ToString().Replace(',', '.')).Append("\"");
            buf.Append(">");
            if (typeOfCurRec > 0)
            {
                buf.Append("<time>").Append(getZuluTime(timeZ, date)).Append("</time>");
                buf.Append("<speed>").Append(speed / 360.0).Append("</speed>");
                if (typeOfCurRec > 1)
                {
                    buf.Append("<ele>").Append(altitude / 10000.0).Append("</ele>");
                }
            }
            buf.Append("</trkpt>");

            return buf.ToString();
        }

        /**
         * Converts this object to its GPX waypoint representation. 
         * @return this object's GPX representation as a String.
         */
        public String ToGpxWpt()
        {
            //		<wpt lat="47.6972383333" lon="11.4178650000">
            //		<ele>662.0000000000</ele>
            //		<time>2007-04-21T13:56:05Z</time>
            //		</wpt>
            var buf = new StringBuilder();
            buf.Append("<wpt");
            buf.Append(" lat=\"").Append(ToDegree(longitude)).Append("\"");
            buf.Append(" lon=\"").Append(ToDegree(longitude)).Append("\"");
            buf.Append(">");
            if (typeOfCurRec > 0)
            {
                buf.Append("<time>").Append(getZuluTime(timeZ, date)).Append("</time>");
                if (typeOfCurRec > 1)
                {
                    buf.Append("<ele>").Append(altitude / 10000.0).Append("</ele>");
                }
            }
            buf.Append("</wpt>");

            return buf.ToString();
        }

        /**
         * Converts GlobalSat latitude and longitude internal format to degrees.
         * @param gsLatOrLon
         * @return nodeg in degrees
         */
        private static double ToDegree(int gsLatOrLon)
        {
            const int Scale = 1000000;

            double degScaled =  (int)gsLatOrLon/Scale;
            var minScaled =(double)((gsLatOrLon - degScaled * Scale) / 600000);
            return degScaled + minScaled;
        }

        /**
         * Gets date and time as a String in given format.
         * @param dateTimeFormat
         * @return 
         */
        private String ToDateTime(String dateTimeFormat)
        {
            int hh = timeZ/10000;
            int mm = (timeZ - hh*10000)/100;
            int ss = timeZ - hh*10000 - mm*100;
            int DD = date/10000;
            int MM = (date - DD*10000)/100;
            int YY = 2000 + (date - DD*10000 - MM*100);

            DateTime dateTime;
            try
            {
                dateTime = new DateTime(YY, MM, DD, hh, mm, ss);
                
            }catch(ArgumentOutOfRangeException)
            {
                dateTime = DateTime.UtcNow;
            }

            return dateTime.ToString(dateTimeFormat);
        }

        /**
         * Gets date and time as a String in GPX date-time-format (aka zulu time).
         * @return a date-time-string in GPX date-time-format (aka zulu time).
         */
        private String getZuluTime(int gsTime, int gsDate)
        {
            return ToDateTime("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        /**
         * Gets date and time as a String in format "yyyyMMddHHmmss".
         * @return a date-time-string in format "yyyyMMddHHmmss".
         */
        public String getDateTimeString()
        {
            return ToDateTime("yyyyMMddHHmmss");
        }

        /**
         * @return Returns the altitude.
         */
        public int getAltitude()
        {
            return altitude;
        }

        /**
         * @param altitude The altitude to set.
         */
        public void setAltitude(int altitude)
        {
            this.altitude = altitude;
        }

        /**
         * @return Returns the date.
         */
        public int getDate()
        {
            return date;
        }

        /**
         * @param date The date to set.
         */
        public void setDate(int date)
        {
            this.date = date;
        }

        /**
         * @return Returns the longitude.
         */
        public int getLongitude()
        {
            return longitude;
        }

        /**
         * @param longitude The longitude to set.
         */
        public void setLongitude(int longitude)
        {
            this.longitude = longitude;
        }

        /**
         * @return Returns the speed.
         */
        public int getSpeed()
        {
            return speed;
        }

        /**
         * @param speed The speed to set.
         */
        public void setSpeed(int speed)
        {
            this.speed = speed;
        }

        /**
         * @return Returns the timeZ.
         */
        public int getTimeZ()
        {
            return timeZ;
        }

        /**
         * @param timeZ The timeZ to set.
         */
        public void setTimeZ(int timeZ)
        {
            this.timeZ = timeZ;
        }

        /**
         * @param latitude The latitude to set.
         */
        public void setLatitude(int latitude)
        {
            this.latitude = latitude;
        }

        #region Utils

        private static byte[] GetShort(int num)
        {
            return num > 0xFF ? new[] { (byte)(num / 100), (byte)(num - 1000) } : new[] { (byte)0, (byte)num };
        }

        private static int GetInt(BinaryReader buf)
        {
            var one = buf.ReadByte();
            var two = buf.ReadByte();
            var three = buf.ReadByte();
            var four = buf.ReadByte();
            return one*0x100*0x100*0x100 + two*0x100*0x100 + three*0x100 + four;
        }

        #endregion

    }
}
