﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Hqub.GlobalSat
{
    /// <summary>
    /// Документация по формату: http://monkeyfood.com/articles/DG100CommsSpec/
    /// 
    /// Sending Format :
    /// 
    /// Start Sequence	Payload Length	Payload Message	    Checksum	End     Sequence
    /// 0xA0  0xA2	      Two-bytes	      Up to 1023	    Two-bytes	0xB0      0xB3
    /// 
    /// Returning Format :
    /// 
    /// Start Sequence	Payload Length	Payload Message	    Checksum	End     Sequence
    /// 0xA0  0xA2	      Two-bytes	      Up to 1023	    Two-bytes	0xB0      0xB3
    /// 
    /// Payload Format:
    /// 
    /// CommandID	  Parameter
    ///  One-byte	n-bytes value

    /// </summary>
    public class GlobalSat
    {
        private const byte Start = 0xA0;
        private const byte SequenceStart = 0xA2;
        private const byte End = 0xB0;
        private const byte SequenceEnd = 0xB3;

        private static bool _isFristOpenPort = true;

        //        private byte [] rawResponse = new byte[65536];
        public SimpleLogger Logger { get; private set; }


        public GlobalSat(string portName = "COM1", int baudRate = 115200)
        {
            Logger = new SimpleLogger();

            PortName = portName;
            BaudRate = baudRate;
        }

        private void SafetyWrite(byte[] buffer, int offset, int count)
        {
            try
            {
                Port.Write(buffer, offset, count);
            }
            catch (IOException ioException)
            {
                Logger.Log(ioException.Message, SimpleLogger.MessageLevel.Error);
            }
        }

        #region Работа с Com-портом

        private SerialPort Port { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        /// <summary>
        /// Открываем порт для чтения данных
        /// </summary>
        /// <returns></returns>
        private bool Open()
        {
            if (Port != null && Port.IsOpen)
            {
                Close();
            }

            Port = new SerialPort(PortName, BaudRate);
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.Parity = Parity.None;

            try
            {
                Port.Open();
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Log(string.Format("Ошибка. {0}", ex.Message), SimpleLogger.MessageLevel.Error);
                return false;
            }
            catch (IOException ex)
            {
                Logger.Log(string.Format("Ошибка. {0} ", ex.Message), SimpleLogger.MessageLevel.Error);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("Ошибка. {0} ", ex.Message), SimpleLogger.MessageLevel.Error);
                return false;
            }

            if (_isFristOpenPort)
            {
                Logger.Log(string.Format(Strings.PORT_OPENED_SUCCESSFULLY, PortName), SimpleLogger.MessageLevel.Success);
                _isFristOpenPort = false;
            }

            return true;
        }

        /// <summary>
        /// Закрываем порт
        /// </summary>
        public void Close()
        {
            try
            {
                if (Port != null)
                {
                    Port.Close();
                    Thread.Sleep(200);
                }
            }
            catch (Exception exception)
            {
                Logger.Log(string.Format("Port.Close; {0}\n{1}", exception.Message, exception.StackTrace), SimpleLogger.MessageLevel.Error);
            }
        }

        public void SetPort(string portName, int baudRate)
        {
            PortName = portName;

            if (Port == null) return;

            Close();

            Port.PortName = portName;
            Port.BaudRate = baudRate;
        }

        #endregion

        /// <summary>
        /// Разбор пришедших данных
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Response ParseResponse(byte[] data)
        {
            if (data.Length <= 0)
                return new Response();

            var response = new Response();
            var wrapData = new BinaryReader(new MemoryStream(data));

            try
            {
                wrapData.BaseStream.Seek(4, SeekOrigin.Begin);
                var respType = wrapData.ReadByte();

                switch (respType)
                {
                    case 0xBB: //Get File Info
                        response.setTypeOfResponse(respType);
                        //                    wrapData.BaseStream.Seek(5, SeekOrigin.Current);

                        var N = wrapData.ReadByte() + wrapData.ReadByte();
                        var hightIndexNextByte = wrapData.ReadByte();
                        var lowIndexNextByte = wrapData.ReadByte();
                        var indexNext = ToShort(lowIndexNextByte, hightIndexNextByte);

                        response.setTypeOfResponse(0xBB);
                        response.setCntDataCur(N);
                        response.setNextIdx(indexNext);

                        for (var i = N; i != 0; --i)
                        {
                            var fileInfoRec = new FileInfoRec(wrapData);
                            response.addRec(fileInfoRec);
                            Console.WriteLine(fileInfoRec);
                        }
                        break;

                    case 0xB5: //GPS Recs

                        response.setTypeOfResponse(respType);

                        var recType = 2;
                        var ii = 0;


                        while (wrapData.BaseStream.Position <= data.Length)
                        {
                            var gpsRec = new GpsRec(wrapData, recType);
                            if (gpsRec.isValid() && wrapData.BaseStream.Position <= data.Length)
                            {
                                response.addRec(gpsRec);
                            }
                            else
                            {
                                Logger.Log(
                                    wrapData.BaseStream.Position + "<=" + data.Length + ", not valid: " +
                                    gpsRec, SimpleLogger.MessageLevel.DebugError);

                                break;
                            }

                            recType = gpsRec.getTypeOfNextRec();
                            ++ii;

                            Logger.Log(ii + ": " + gpsRec, SimpleLogger.MessageLevel.Debug);
                        }

                        wrapData.BaseStream.Seek(1042, SeekOrigin.Begin);

                        while (wrapData.BaseStream.Position <= data.Length)
                        {
                            if ((recType == 2 && (data.Length - wrapData.BaseStream.Position) < 32) ||
                                (recType == 1 && (data.Length - wrapData.BaseStream.Position) < 20) ||
                                (recType == 0 && (data.Length - wrapData.BaseStream.Position) < 8))
                            {
                                break;
                            }


                            var gpsRec = new GpsRec(wrapData, recType);
                            if (gpsRec.isValid() && wrapData.BaseStream.Position <= data.Length)
                            {
                                response.addRec(gpsRec);
                            }
                            else
                            {
                                Logger.Log(
                                    wrapData.BaseStream.Position + "<=" + data.Length + ", not valid: " +
                                    gpsRec, SimpleLogger.MessageLevel.DebugError);

                                break;
                            }

                            recType = gpsRec.getTypeOfNextRec();
                            ++ii;

                            Logger.Log(ii + ": " + gpsRec, SimpleLogger.MessageLevel.Debug);
                        }

                        break;

                    case 0xBF: //Get Id
                    case 0xDF:
                        response.Id = 0;

                        for (var i = 0; i < 8; ++i)
                        {
                            var digit = wrapData.ReadByte();
                            response.Id = response.Id * 10 + digit;
                        }

                        break;

                    case 0xC0:

                        break;

                    case 0xBA:
                        response.setTypeOfResponse(respType);

                        var value = GetInt(wrapData);

                        if (value == 1)
                        {
                            Logger.Log("Память успешно очищена.", SimpleLogger.MessageLevel.Success);
                        }
                        else
                        {
                            Logger.Log("Во время очистки памяти произошла ошибка.", SimpleLogger.MessageLevel.Error);
                        }

                        break;
                }

                return response;
            }
            catch (EndOfStreamException endOfStreamException)
            {
                Logger.Log(string.Format("Чтение за пределами потока.\n{0}", endOfStreamException.Message),
                           SimpleLogger.MessageLevel.Error);

                return null;
            }
            catch (Exception exception)
            {
                Logger.Log(string.Format("Необработное исключение в ParseResponse.\n{0}", exception.Message),
                           SimpleLogger.MessageLevel.Error);
                return null;
            }
        }

        #region Read Response

        /// <summary>
        /// Читаем заголовки трек файлов
        /// </summary>
        /// <returns></returns>
        private IEnumerable<object> ReadFileInfoList()
        {
            var nextIndex = 0;
            var fileInfoList = new List<object>();

            do
            {
                var splitIndex = GetShort(nextIndex);
                var response = SendGetTrackFileHeader(splitIndex[0], splitIndex[1]);
                if (response == null)
                    break;

                nextIndex = response.getNextIdx();
                fileInfoList.AddRange(response.getRecs());

            } while (nextIndex > 0);

            return fileInfoList;
        }

        /// <summary>
        /// Читаем gps-записиы
        /// </summary>
        /// <param name="fileInfoRecs">Список трэк-файлов</param>
        /// <returns></returns>
        private IEnumerable<object> ReadGpsRecList(IEnumerable<object> fileInfoRecs)
        {
            //            var firstIter = true;
            var gpsRecs = new List<object>();

            foreach (var fileInfoRec in fileInfoRecs)
            {
                var index = ((FileInfoRec)fileInfoRec).getIdx();
                var splitIndex = GetShort(index);
                var response = SendGetGpsRecs(splitIndex[0], splitIndex[1]);
                if (response == null)
                    break;

                Logger.Log(string.Format(Strings.TRACK_LOADED, index + 1, response.getRecs().Count),
                           SimpleLogger.MessageLevel.Information);

                gpsRecs.AddRange(response.getRecs());

            }

            return gpsRecs;
        }

        #endregion

        #region Методы для отправки запросов на получения данных

        private Response SendGetId()
        {
            const byte CommandId = 0xBF;

            Logger.Log("Получаем ID устройства", SimpleLogger.MessageLevel.Information);

            if (!Open())
            {

                return null;
            }

            var payload = new[] { CommandId };

            var checksum = GetShort(Checksum(payload));

            var buffer = new[]
                             {
                                 (byte) 0xA0, (byte) 0xA2, (byte) 0x00, (byte) 0x01, CommandId
                                 , checksum[0], checksum[1], (byte) 0xB0, (byte) 0xB3
                             };

            SafetyWrite(buffer, 0, buffer.Length);
            Sleep();

            if (Port.BytesToRead == 0)
                return null;

            var rawResponse = new byte[Port.BytesToRead];
            Port.Read(rawResponse, 0, Port.BytesToRead);

            return ParseResponse(rawResponse);
        }

        private Response SendSetId(byte[] id)
        {
            const byte CommandId = 0xC0;

            Logger.Log("Идет установка ID устройству ...", SimpleLogger.MessageLevel.Information);

            if (!Open())
            {
                Logger.Log(string.Format(Strings.PORT_BLOCKED, PortName), SimpleLogger.MessageLevel.Error);
                return null;
            }

            var payload = new[] { CommandId, id[0], id[1], id[2], id[3], id[4], id[5], id[6], id[7] };

            var checksum = GetShort(Checksum(payload));

            var buffer = new[]
                             {
                                 (byte) 0xA0, (byte) 0xA2, (byte) 0x00, (byte) 0x09, CommandId,
                                 id[0], id[1], id[2], id[3], id[4], id[5], id[6], id[7],
                                 checksum[0], checksum[1], (byte) 0xB0, (byte) 0xB3
                             };

            SafetyWrite(buffer, 0, buffer.Length);
            Sleep();

            var rawResponse = new byte[Port.BytesToRead];
            Port.Read(rawResponse, 0, Port.BytesToRead);

            return ParseResponse(rawResponse);

        }

        /// <summary>
        /// Отправляем запрос на получение заголовков.
        /// </summary>
        private Response SendGetTrackFileHeader(byte hightIndex, byte lowIndex)
        {
            const byte CommandId = 0xBB;

            if (!Open())
            {
                Logger.Log(string.Format(Strings.PORT_BLOCKED, PortName), SimpleLogger.MessageLevel.Error);
                return null;
            }

            var payload = new[]
                              {
                                  CommandId, /*Index:*/ hightIndex,
                                  lowIndex /**/
                              };

            Logger.Log(Strings.GetHeader, SimpleLogger.MessageLevel.Information);
            var checksum = Checksum(payload);
            var splitChecksum = GetShort(checksum);

            var buffer = new byte[]
                             {
                                 Start, SequenceStart, /*Length*/0x00, 0x03 /**/, CommandId, /*Index:*/ hightIndex,
                                 lowIndex, /**/ splitChecksum[0], splitChecksum[1], End, SequenceEnd
                             };

            SafetyWrite(buffer, 0, buffer.Length);

            Sleep(2000);

            var rawResponse = new byte[Port.BytesToRead];
            Port.Read(rawResponse, 0, Port.BytesToRead);

            return ParseResponse(rawResponse);
        }

        /// <summary>
        /// Отправляем запрос на получение точек
        /// </summary>
        private Response SendGetGpsRecs(byte hightIndex = (byte)0, byte lowIndex = (byte)0)
        {
            const byte CommandId = 0xB5;

            if (!Open())
            {
                Logger.Log(string.Format(Strings.PORT_BLOCKED, PortName), SimpleLogger.MessageLevel.Error);
                return null;
            }

            var payload = new[]
                              {
                                  CommandId, /*Index:*/ hightIndex,
                                  lowIndex /**/
                              };

            //            Logger.Log(Strings.GetGpsRecs, SimpleLogger.MessageLevel.Information);
            var checksum = Checksum(payload);
            var splitChecksum = GetShort(checksum);

            var buffer = new byte[]
                             {
                                 Start, SequenceStart, /*Length*/0x00, 0x03 /**/, CommandId, /*Index:*/ hightIndex,
                                 lowIndex, /**/ splitChecksum[0], splitChecksum[1], End, SequenceEnd
                             };

            SafetyWrite(buffer, 0, buffer.Length);

            Sleep();

            var rawResponse = new byte[Port.ReadBufferSize];
            Port.Read(rawResponse, 0, Port.ReadBufferSize);

            return ParseResponse(rawResponse);
        }

        /// <summary>
        /// Стирает все треки из памяти аппарата
        /// </summary>
        /// <returns></returns>
        public void SendClear()
        {
            const byte CommandId = 0xBA;
            const int Offset = 0;

            if (!Open())
            {

                Logger.Log(string.Format(Strings.PORT_BLOCKED, PortName), SimpleLogger.MessageLevel.Error);
                return;
            }

            var payload = new byte[]
                              {
                                  CommandId, /*Index:*/ 0xFF,
                                  0xFF /**/
                              };

            //            Logger.Log(Strings.GetGpsRecs, SimpleLogger.MessageLevel.Information);
            var checksum = GetShort(Checksum(payload));

            var buffer = new byte[] { 160, 0xa2, 0, 3, 0xba, 0xff, 0xff, 2, 0xb8, 0xb0, 0xb3 };
            Logger.Log("Erasing memory start", SimpleLogger.MessageLevel.Information);

            SafetyWrite(buffer, Offset, buffer.Length);

            Sleep(500);

            var rawResponse = new byte[Port.BytesToRead];
            Port.Read(rawResponse, 0, Port.BytesToRead);

            ParseResponse(rawResponse);
        }

        #endregion

        #region User Interface

        public string ExportToGpx()
        {
            var fileInfoList = ReadFileInfoList();

            if (((IList<object>)fileInfoList).Count == 0)
            {
                Logger.Log("Треки не найдены.", SimpleLogger.MessageLevel.Information);
                return string.Empty;
            }

            var gpsRecList = ReadGpsRecList(fileInfoList);

            if (((IList<object>)gpsRecList).Count == 0)
                return string.Empty;

            var gpxFileName = string.Format("Export_{0}.gpx", DateTime.Now);
            var strBuffer = new StringBuilder();


            strBuffer.Append("<gpx\n");
            strBuffer.Append("  version=\"1.0\"\n");
            strBuffer.Append("  creator=\"dg100util by Glebov Boris\"\n");
            strBuffer.Append("  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
            strBuffer.Append("  xmlns=\"http://www.topografix.com/GPX/1/0\"\n");
            strBuffer.Append("  xsi:schemaLocation=\"http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd\">\n");
            strBuffer.Append("  <name>" + gpxFileName + "</name>\n");

            //waypoints
            var wayPoints = new List<GpsRec>();
            GpsRec prevGpsRec = null;

            foreach (var gpsRec in gpsRecList)
            {
                var wrapGpsRec = (GpsRec)gpsRec;

                if (wrapGpsRec.equals(prevGpsRec))
                {
                    wayPoints.Add(wrapGpsRec);
                }
                prevGpsRec = wrapGpsRec;
            }

            foreach (var wayPoint in wayPoints)
            {
                strBuffer.Append(wayPoint.ToGpxWpt() + "\n");
            }

            //track
            strBuffer.Append("  <trk>\n");
            strBuffer.Append("    <trkseg>\n");

            foreach (var gpsRec in gpsRecList)
            {
                var wrapGpsRec = (GpsRec)gpsRec;
                strBuffer.Append(wrapGpsRec.ToGpxTrkpt() + "\n");
            }

            strBuffer.Append("    </trkseg>\n");
            strBuffer.Append("  </trk>\n");
            strBuffer.Append("</gpx>\n");

            //Закрываем порт:
            Close();

            return strBuffer.ToString();
        }

        /// <summary>
        /// Получаем Id устройства
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            var response = SendGetId();

            if (response == null)
                return -1;

            var id = response.Id;

            return id;
        }

        /// <summary>
        /// Установить Id устройству
        /// </summary>
        /// <param name="Id"></param>
        public void SetId(int Id)
        {
            var buffer = new byte[8];

            var strId = Id.ToString();
            for (var i = 0; i < strId.Length; i++)
            {
                buffer[strId.Length - i - 1] = byte.Parse(strId[i].ToString());
            }

            //Inverse
            var inversBuffer = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                inversBuffer[i] = buffer[8 - i - 1];
            }


            var response = SendSetId(inversBuffer);

            Close();
        }

        #endregion

        #region Utils

        private static byte[] GetShort(int num)
        {
            var byte2 = (byte)(num >> 8);
            var byte1 = (byte)(num & 255);

            return new[] { byte2, byte1 };

            //return num > 0xFF ? new[] { (byte)(num / 100), (byte)(num - 1000) } : new[] { (byte)0, (byte)num };
        }

        private static short ToShort(short byte1, short byte2)
        {
            return (short)((byte2 << 8) + byte1);
        }

        private int GetInt(BinaryReader buf)
        {
            return buf.ReadByte() * 0x100 * 0x100 * 0x100 + buf.ReadByte() * 0x100 * 0x100 + buf.ReadByte() * 0x100 + buf.ReadByte();
        }

        private static short ToShort(byte[] buf)
        {
            return (short)(buf[0] * 0x100 + buf[0]);
        }

        private static int ToInt(byte[] buf)
        {
            throw new NotImplementedException();
        }

        private int Checksum(byte[] array)
        {
            int checksum = array[0];
            var j = 1;
            for (var i = 1; i < array.Length; i++)
            {
                checksum = checksum + array[i];
            }

            return checksum & ((1 << 15) - 1);
        }

        private void Sleep(int time = 200)
        {
            System.Threading.Thread.Sleep(time);
        }

        #endregion

    }
}
