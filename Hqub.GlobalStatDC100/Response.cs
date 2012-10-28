using System;
using System.Collections.Generic;

namespace Hqub.GlobalSat
{
    public class Response
    {
        private int typeOfResponse = 0;
        private int cntDataCur = 0;
        private int nextIdx = 0;
        private Config config = null;
        private List<object> data = new List<object>(100);
        private GpsRec minGpsRec = null;
        private GpsRec maxGpsRec = null;
        private int _id;
        private bool _isNull;

        public void addRec(Object obj)
        {
            data.Add(obj);
        }

        public List<object> getRecs()
        {
            return data;
        }

        /**
         * @return Returns the cntDataCur.
         */
        public int getCntDataCur()
        {
            return cntDataCur;
        }

        /**
         * @return Returns the nextIdx.
         */
        public int getNextIdx()
        {
            return nextIdx;
        }

        /**
         * @return Returns the typeOfResponse.
         */
        public int getTypeOfResponse()
        {
            return typeOfResponse;
        }

        /**
         * @param typeOfResponse The typeOfResponse to set.
         */
        public void setTypeOfResponse(int typeOfResponse)
        {
            this.typeOfResponse = typeOfResponse;
        }

        /**
         * @param cntDataCur The cntDataCur to set.
         */
        public void setCntDataCur(int cntDataCur)
        {
            this.cntDataCur = cntDataCur;
        }

        /**
         * @param nextIdx The nextIdx to set.
         */
        public void setNextIdx(int nextIdx)
        {
            this.nextIdx = nextIdx;
        }

        /**
         * @return Returns the config.
         */
        public Config getConfig()
        {
            return config;
        }

        /**
         * @param config The config to set.
         */
        public void setConfig(Config config)
        {
            this.config = config;
        }

        /**
         * @return Returns the maxGpsRec.
         */
        public GpsRec getMaxGpsRec()
        {
            return maxGpsRec;
        }

        /**
         * @return Returns the minGpsRec.
         */
        public GpsRec getMinGpsRec()
        {
            return minGpsRec;
        }

        /**
         * @param maxGpsRec The maxGpsRec to set.
         */
        public void initMinMaxGpsRec(GpsRec init)
        {
            minGpsRec = new GpsRec(init);
            maxGpsRec = new GpsRec(init);
        }

        /// <summary>
        /// Id Device
        /// </summary>
        public int Id { get; set; }

    }
}
