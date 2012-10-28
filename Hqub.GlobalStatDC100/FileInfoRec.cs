using System;
using System.IO;

namespace Hqub.GlobalSat
{
    public class FileInfoRec
    {
        private int timeZ = 0;
        private int date = 0;
        private int idx = 0;

        public FileInfoRec(BinaryReader buf)
        {
            timeZ = GetInt(buf);
            date = GetInt(buf);
            idx = GetInt(buf);
        }

        private int GetInt(BinaryReader buf)
        {
            try
            {
                return buf.ReadByte()*0x100*0x100*0x100 + buf.ReadByte()*0x100*0x100 + buf.ReadByte()*0x100 +
                       buf.ReadByte();
            }catch(Exception)
            {
                return 0;
            }
        }

        public override String ToString()
        {
            return "[FileInfoRec: timeZ = " + timeZ + ", date = " + ParseDate(date) + ", idx = " + idx + "]";

        }

        private static string ParseTime(int rawTime)
        {
            var hh = (rawTime / 10000) + 3;
            var mm = (rawTime - hh * 10000) / 100;
            var ss = rawTime - hh * 10000 - mm * 100;
            
            return string.Format("{0}:{1}:{2}", hh, mm, ss);
        }

        private static string ParseDate(int rawDate)
        {
            int DD = rawDate / 10000;
		    int MM = (rawDate - DD * 10000) / 100;
		    int YY = rawDate - DD * 10000 - MM * 100;

            var d = DateTime.FromBinary(rawDate);
            return string.Format("{0}.{1}.{2}", DD, MM, YY);
        }

        /**
         * @return Returns the idx.
         */
        public int getIdx()
        {
            return idx;
        }
    }
}
