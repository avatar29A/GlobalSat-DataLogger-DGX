using System;
using System.IO;

namespace Hqub.GlobalSat
{
    public class Config
    {
        //  aa aa aa aa: time in ms for switch a
        //  bb bb bb bb: time in ms for switch b
        //  cc cc cc cc: time in ms for switch c
        //  dd dd dd dd: distance in m for switch a
        //  ee ee ee ee: distance in m for switch b
        //  ff ff ff ff: distance in m for switch c
        //  gg: 00=time, 01=distance for switch a
        //  hh: 00=time, 01=distance for switch b
        //  ii: 00=time, 01=distance for switch c
        //  jj: 00=position; 01=position,date,time,speed; 02=position,date,time,speed,altitude
        //  kk: 01=disable logging if speed < ll ll ll ll
        //  ll ll ll ll: speed in km/h * 100
        //  mm: 01=disable logging if distance < nn nn nn nn;
        //  nn nn nn nn: distance in m
        //  xx xx: checksum
        //	A0 A2 00 35 B7 jj kk ll ll ll ll mm nn nn nn nn
        //  aa aa aa aa bb bb bb bb cc cc cc cc 00 00 gg hh
        //  ii dd dd dd dd ee ee ee ee ff ff ff ff 01 61 01
        //  01 0C D5 0D 00 04 CC B0 B3

        private sbyte logFormat = -1;
        private sbyte disableLogSpeed = -1;
        private int speedThres = -1;
        private sbyte disableLogDist = -1;
        private int distThres = -1;
        private int swATime = -1;
        private int swBTime = -1;
        private int swCTime = -1;
        private short unk1 = -1;
        private sbyte swATimeOrDist = -1;
        private sbyte swBTimeOrDist = -1;
        private sbyte swCTimeOrDist = -1;
        private int swADist = -1;
        private int swBDist = -1;
        private int swCDist = -1;
        private sbyte unk2 = -1;
        private int remainder = -1;
        private int unk3 = -1;
        private int unk4 = -1;

        public Config(BinaryReader buf)
        {
            logFormat = buf.ReadSByte();
            disableLogSpeed = buf.ReadSByte();
            speedThres = buf.ReadInt32();
            disableLogDist = buf.ReadSByte();
            distThres = buf.ReadInt32();
            swATime = buf.ReadByte();
            swBTime = buf.ReadByte();
            swCTime = buf.ReadByte();
            unk1 = buf.ReadInt16();
            swATimeOrDist = buf.ReadSByte();
            swBTimeOrDist = buf.ReadSByte();
            swCTimeOrDist = buf.ReadSByte();
            swADist = buf.ReadInt32();
            swBDist = buf.ReadInt32();
            swCDist = buf.ReadInt32();
            unk2 = buf.ReadSByte();
            remainder = buf.ReadByte();
            unk3 = buf.ReadByte();
            unk4 = buf.ReadByte();
        }

        public String toString()
        {
            return
                "[Config: logFormat = " + logFormat
                + ",disableLogSpeed = " + disableLogSpeed
                + ",speedThres = " + speedThres
                + ",disableLogDist = " + disableLogDist
                + ",distThres = " + distThres
                + ",swATime = " + swATime
                + ",swBTime = " + swBTime
                + ",swCTime = " + swCTime
                + ",unk1 = " + unk1
                + ",swATimeOrDist = " + swATimeOrDist
                + ",swBTimeOrDist = " + swBTimeOrDist
                + ",swCTimeOrDist = " + swCTimeOrDist
                + ",swADist = " + swADist
                + ",swBDist = " + swBDist
                + ",swCDist = " + swCDist
                + ",unk2 = " + unk2
                + ",remainder = " + remainder
                + ",unk3 = " + unk3
                + ",unk4 = " + unk4
                ;
        }

        /**
         * @param buf
         */
        public void write(BinaryWriter buf)
        {
            buf.BaseStream.Seek(5, SeekOrigin.Begin);
            buf.Write(logFormat);
            buf.Write(disableLogSpeed);
            buf.Write(speedThres);
            buf.Write(disableLogDist);
            buf.Write(distThres);
            buf.Write(swATime);
            buf.Write(swBTime);
            buf.Write(swCTime);
            buf.Write(unk1);
            buf.Write(swATimeOrDist);
            buf.Write(swBTimeOrDist);
            buf.Write(swCTimeOrDist);
            buf.Write(swADist);
            buf.Write(swBDist);
            buf.Write(swCDist);
            buf.Write(unk2);
        }

        /**
         * @return Returns the disableLogDist.
         */
        public sbyte getDisableLogDist()
        {
            return disableLogDist;
        }

        /**
         * @param disableLogDist The disableLogDist to set.
         */
        public void setDisableLogDist(sbyte disableLogDist)
        {
            this.disableLogDist = disableLogDist;
        }

        /**
         * @return Returns the disableLogSpeed.
         */
        public sbyte getDisableLogSpeed()
        {
            return disableLogSpeed;
        }

        /**
         * @param disableLogSpeed The disableLogSpeed to set.
         */
        public void setDisableLogSpeed(sbyte disableLogSpeed)
        {
            this.disableLogSpeed = disableLogSpeed;
        }

        /**
         * @return Returns the distThres.
         */
        public int getDistThres()
        {
            return distThres;
        }

        /**
         * @param distThres The distThres to set.
         */
        public void setDistThres(int distThres)
        {
            this.distThres = distThres;
        }

        /**
         * @return Returns the logFormat.
         */
        public sbyte getLogFormat()
        {
            return logFormat;
        }

        /**
         * @param logFormat The logFormat to set.
         */
        public void setLogFormat(sbyte logFormat)
        {
            this.logFormat = logFormat;
        }

        /**
         * @return Returns the speedThres.
         */
        public int getSpeedThres()
        {
            return speedThres;
        }

        /**
         * @param speedThres The speedThres to set.
         */
        public void setSpeedThres(int speedThres)
        {
            this.speedThres = speedThres;
        }

        /**
         * @return Returns the swADist.
         */
        public int getSwADist()
        {
            return swADist;
        }

        /**
         * @param swADist The swADist to set.
         */
        public void setSwADist(int swADist)
        {
            this.swADist = swADist;
        }

        /**
         * @return Returns the swATime.
         */
        public int getSwATime()
        {
            return swATime;
        }

        /**
         * @param swATime The swATime to set.
         */
        public void setSwATime(int swATime)
        {
            this.swATime = swATime;
        }

        /**
         * @return Returns the swATimeOrDist.
         */
        public sbyte getSwATimeOrDist()
        {
            return swATimeOrDist;
        }

        /**
         * @param swATimeOrDist The swATimeOrDist to set.
         */
        public void setSwATimeOrDist(sbyte swATimeOrDist)
        {
            this.swATimeOrDist = swATimeOrDist;
        }

        /**
         * @return Returns the swBDist.
         */
        public int getSwBDist()
        {
            return swBDist;
        }

        /**
         * @param swBDist The swBDist to set.
         */
        public void setSwBDist(int swBDist)
        {
            this.swBDist = swBDist;
        }

        /**
         * @return Returns the swBTime.
         */
        public int getSwBTime()
        {
            return swBTime;
        }

        /**
         * @param swBTime The swBTime to set.
         */
        public void setSwBTime(int swBTime)
        {
            this.swBTime = swBTime;
        }

        /**
         * @return Returns the swBTimeOrDist.
         */
        public sbyte getSwBTimeOrDist()
        {
            return swBTimeOrDist;
        }

        /**
         * @param swBTimeOrDist The swBTimeOrDist to set.
         */
        public void setSwBTimeOrDist(sbyte swBTimeOrDist)
        {
            this.swBTimeOrDist = swBTimeOrDist;
        }

        /**
         * @return Returns the swCDist.
         */
        public int getSwCDist()
        {
            return swCDist;
        }

        /**
         * @param swCDist The swCDist to set.
         */
        public void setSwCDist(int swCDist)
        {
            this.swCDist = swCDist;
        }

        /**
         * @return Returns the swCTime.
         */
        public int getSwCTime()
        {
            return swCTime;
        }

        /**
         * @param swCTime The swCTime to set.
         */
        public void setSwCTime(int swCTime)
        {
            this.swCTime = swCTime;
        }

        /**
         * @return Returns the swCTimeOrDist.
         */
        public sbyte getSwCTimeOrDist()
        {
            return swCTimeOrDist;
        }

        /**
         * @param swCTimeOrDist The swCTimeOrDist to set.
         */
        public void setSwCTimeOrDist(sbyte swCTimeOrDist)
        {
            this.swCTimeOrDist = swCTimeOrDist;
        }

        /**
         * @return Returns the unk1.
         */
        public short getUnk1()
        {
            return unk1;
        }

        /**
         * @param unk1 The unk1 to set.
         */
        public void setUnk1(short unk1)
        {
            this.unk1 = unk1;
        }

        /**
         * @return Returns the unk2.
         */
        public sbyte getUnk2()
        {
            return unk2;
        }

        /**
         * @param unk2 The unk2 to set.
         */
        public void setUnk2(sbyte unk2)
        {
            this.unk2 = unk2;
        }

        /**
         * @return Returns the remainder.
         */
        public int getRemainder()
        {
            return remainder;
        }

        /**
         * @return Returns the unk3.
         */
        public int getUnk3()
        {
            return unk3;
        }

        /**
         * @return Returns the unk4.
         */
        public int getUnk4()
        {
            return unk4;
        }
    }
}
