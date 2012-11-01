using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using Hqub.GlobalSat;
using Hqub.GlobalStatDC100.Host.Dialogs;
using System.IO.Ports;
using Hqub.GlobalStatDC100.Host.Properties;

namespace Hqub.GlobalStatDC100.Host
{
    public partial class MainForm : Form
    {
        private readonly GlobalSat.GlobalSat _device;
        private StringBuilder _log = new StringBuilder();
        private string _comPort;
        private readonly int _baudRate;


        private readonly NLog.Logger Log = NLog.LogManager.GetLogger("isupervise");

        public MainForm()
        {
            InitializeComponent();

            System.Net.ServicePointManager.Expect100Continue = false; 

            _comPort = ConfigHelper.Port;
            _baudRate = ConfigHelper.BaudRate;

            _device = new GlobalSat.GlobalSat(_comPort, _baudRate);
            
            DetectDeviceModel();

            Closing += MainForm_Closing;
            
            _device.Logger.EventLog += LoggerEventLog;

			Prepare();
        }

		/// <summary>
		/// Подготовка к работе приложения.
		/// Создает каталоги log и export если отсуствуют
		/// </summary>
		private void Prepare()
		{
			try
			{
				var exportPath = Application.StartupPath + "\\export";
				var logPath = Application.StartupPath + "\\logs";

				if (!Directory.Exists(exportPath))
				{
					Directory.CreateDirectory(exportPath);
				}

				if (!Directory.Exists(logPath))
				{
					Directory.CreateDirectory(logPath);
				}
			}
			catch (Exception ex)
			{
				_device.Logger.Log(string.Format("Ошибка в методе 'Prepare()'. {0}", ex.Message), SimpleLogger.MessageLevel.Error);
			}
		}

        protected override void OnShown(EventArgs e)
        {  
            //Определяем com-порт:
            toolStatusLabel.Text = Strings.AUTO_DETECTION_PORT;

            // Выставляет значение авто очистки:
            cbAutoClear.Checked = ConfigHelper.AutoClear;

            //Обновляем статус и перерисовываем форму:
            Update();
          
            _comPort = _device.PortName = AutoDetectComPort();

            //Отмечаем выбранный порт:
            SetPortCheckedValue(_device.PortName, true);
            _device.Logger.Log(string.Format("Порт: {0}", _device.PortName), SimpleLogger.MessageLevel.Information);

            toolStatusLabel.Text = Strings.CONNECT_TO_SERVER;
            
            Update();

            Ping();

            //Определяем соединение с сервером
            toolStatusLabel.Text = Strings.SUCCESS;
        }

        private void LoggerEventLog(string message, SimpleLogger.MessageLevel messagelevel)
        {
            switch (messagelevel)
            {
                case SimpleLogger.MessageLevel.None:
                    AddItemToConsole(message, Color.Black);
                    Log.Trace(message);
                    break;
                case SimpleLogger.MessageLevel.Success:
                    AddItemToConsole(message, Color.Green);
                    Log.Trace(message);
                    break;
                case SimpleLogger.MessageLevel.Information:
                    AddItemToConsole(message, Color.Blue);
                    Log.Info(message);
                    break;

                case SimpleLogger.MessageLevel.Error:
                    AddItemToConsole(message, Color.Red);
                    Log.Error(message);
                    break;
            }

            Update();
        }

        /// <summary>
        /// Запускает процедуру экспорта
        /// </summary>
        private void ExportClick(object sender, EventArgs e)
        {
            toolStripMenuItem2.Text = Strings.EXPORT_DATA_PROCEED;

            _log = new StringBuilder();
            _log.AppendLine("<log>");

            Cursor.Current = Cursors.WaitCursor;

            var buldExportStream = new StringBuilder();
            buldExportStream.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            buldExportStream.Append("<export>\n");

            var deviceId = _device.GetId();

            //Ошибка при обработке com-порта
            if(deviceId == -1)
            {
                _device.Logger.Log("Устройство не подключено.", SimpleLogger.MessageLevel.Error);
                return;
            }

            _log.AppendFormat("<device ID=\"{0}\" />", deviceId);
            _device.Logger.Log(string.Format("ID = {0}", deviceId), SimpleLogger.MessageLevel.Information);

            buldExportStream.AppendFormat("<device ID=\"{0}\" />\n", deviceId);
            buldExportStream.Append(_device.ExportToGpx());
            buldExportStream.Append("</export>");

            var exportStream = buldExportStream.ToString();
            //Сохраняем полученные данные в файл:
            SaveExportStream(exportStream, deviceId);

            try
            {
               var resp = PostRequestUtils.FileRequest.UploadFile(ConfigHelper.ExportUrl, ConfigHelper.ExportArgumentName, exportStream).Split(';');

                _device.Logger.Log(string.Format("Выгрузка завершена."), SimpleLogger.MessageLevel.Information);

                if (resp.Length >= 2)
                {
                    _device.Logger.Log(string.Format("Ответ сервера: {0} ({1})", resp[1], resp[0]),
                                       SimpleLogger.MessageLevel.Information);

                    // Если сервер обработал запрос, то очищаем память устройства:
                    if (resp[0].Trim().ToLower() == "true")
                    {
                        // Очищаем память устройства.
                        if (cbAutoClear.Checked)
                            ClearMemory(); 
                    }
                }
            }
            catch (Exception exception)
            {
                Cursor.Current = Cursors.Arrow;
                _device.Logger.Log(exception.ToString(), SimpleLogger.MessageLevel.Error);

                return;
            }

            Cursor.Current = Cursors.Arrow;

            _log.AppendLine("</log>");

            //Отправляем лог на сервер:
//            Cursor.Current = Cursors.WaitCursor;
//
//            toolStripMenuItem2.Text = Strings.LoadLogFileToServer;

//            try
//            {
//
//                var respLogUpload = PostRequestUtils.FileRequest.UploadFile(ConfigHelper.LogUrl,
//                                                                            ConfigHelper.LogArgumentName,
//                                                                            _log.ToString()).Split(';');
//
//                if (respLogUpload.Length >= 2)
//                {
//                    var status = bool.Parse(respLogUpload[0].Trim());
//
//                    _device.Logger.Log(respLogUpload[1], !status ? SimpleLogger.MessageLevel.Error : SimpleLogger.MessageLevel.Success);
//                }
//                else
//                {
//                    _device.Logger.Log("На сервере произошла ошибка.", SimpleLogger.MessageLevel.Error);
//                }
//
//                toolStripMenuItem2.Text = Strings.SUCCESS;
//                Cursor.Current = Cursors.Arrow;
//
//                //Сохраняем лог в файл:
//                SaveLogStream(deviceId, _log.ToString());
//            }
//            catch (Exception exception)
//            {
//                MessageBox.Show(string.Format("Ошибка. {0}", exception.Message));
//            }
        }

        /// <summary>
        /// Сохраняем полученные с устройства данные в файл
        /// </summary>
        private void SaveExportStream(string exportStream, int deviceId)
        {
            if (string.IsNullOrEmpty(exportStream))
            {
                _device.Logger.Log("Не удалось сохранить экспорт. Поток пуст.", SimpleLogger.MessageLevel.Error);
                return;
            }

            if (!Directory.Exists("export"))
            {
                _device.Logger.Log("Не удалось сохранить данные. Каталог 'export' не найден.",
                                   SimpleLogger.MessageLevel.Error);
                return;
            }

            var exportPath = string.Format("{0}export\\{1}", AppDomain.CurrentDomain.BaseDirectory, deviceId);
            if(!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            var path = string.Format("{0}\\export_{1}.xml", exportPath ,DateTime.Now.ToString().Replace(':', '-'));
            File.WriteAllText(path, exportStream);

            _device.Logger.Log(string.Format("Данные успешно сохранены. Путь: '{0}'.", path), SimpleLogger.MessageLevel.Success);
        }

        /// <summary>
        /// Сохраняем лог в файл
        /// </summary>
        private void SaveLogStream(int deviceId, string logStream)
        {
            if (string.IsNullOrEmpty(logStream))
            {
                _device.Logger.Log("Не удалось сохранить экспорт. Поток пуст.", SimpleLogger.MessageLevel.Error);
                return;
            }

            if (!Directory.Exists("logs"))
            {
                _device.Logger.Log("Не удалось сохранить данные. Каталог 'logs' не найден.",
                                   SimpleLogger.MessageLevel.Error);
                return;
            }

            var path = string.Format("logs//log_deviceId-{0}_{1}.xml", deviceId,
                                     DateTime.Now.ToString().Replace(':', '-'));
            File.WriteAllText(path, logStream);

            _device.Logger.Log(string.Format("Лог успешно сохранен. Путь: '{0}'.", path), SimpleLogger.MessageLevel.Success);
        }

        /// <summary>
        /// Очищаем память устройства
        /// </summary>
        private void ClearClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(Strings.QUESTION_CLEAR_DEVICE,
            Strings.CAPTION_CLEAR_DEVICE,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            ClearMemory();
        }

        /// <summary>
        /// Удаляем все из памяти аппарата:
        /// </summary>
        private void ClearMemory()
        {
            toolStripMenuItem2.Text = Strings.CLEAR_MEMORY;
            Cursor = Cursors.WaitCursor;
            _device.SendClear();
            Cursor = Cursors.Arrow;
            toolStripMenuItem2.Text = Strings.SUCCESS;
        }

        #region Устройство

        /// <summary>
        /// Автоматически определяем порт устройства
        /// </summary>
        /// <returns></returns>
        private string AutoDetectComPort()
        {
            var portNames = new List<string>(SerialPort.GetPortNames());

            // Если не получилось извлечь список ком. портов из HKLM\Hardware\DeviceMap\SerialComm,
            // то составляем их искусственно:
            if (portNames.Count == 0)
                for (var i = 1; i <= ConfigHelper.MaxPorts; i++)
                    portNames.Add(string.Format("COM{0}", i));

            var tempDevice = new GlobalSat.GlobalSat(baudRate: ConfigHelper.BaudRate);
            try
            {
                var portFind = false;
                for (var i = 0; i < portNames.Count; i++)
                {
                    tempDevice.SetPort(portNames[i], ConfigHelper.BaudRate);
                    var id = tempDevice.GetId();

                    if (id == -1)
                    {
                        continue;
                    }

                    portFind = true;
                    break;
                }

                tempDevice.Close();

                if (portFind)
                {
                    return tempDevice.PortName;
                }

                AddItemToConsole("Устройство либо не подключено, либо настроено неверно.", Color.Red);
                return ConfigHelper.Port;
            }
            catch
            {
                AddItemToConsole("Устройство либо не подключено, либо настроено неверно.", Color.Red);
                return ConfigHelper.Port;
            }
        }

        /// <summary>
        /// Установить id-устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetIdClick(object sender, EventArgs e)
        {
//            if(!Autorization())
//                return;

            var dialog = new SetIdDeviceDialog();
            dialog.ShowDialog();

            if (dialog.Id == 0) return;

            _device.SetId(dialog.Id);

            AddItemToConsole(string.Format("Новый ID устройства: {0}", _device.GetId()), Color.Green);
        }

        /// <summary>
        /// Получить id-устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetIdClick(object sender, EventArgs e)
        {
            AddItemToConsole(string.Format("ID устройства: {0}", _device.GetId()), Color.Blue);
        }
       
        /// <summary>
        /// Определяем модель устройства
        /// </summary>
        private void DetectDeviceModel()
        {
            if (ConfigHelper.BaudRate == Settings.Default.DG200BaudRate)
            {
                rbGlobalSatDG200.Checked = true;
            }
            else
            {
                rbGlobalSatDG100.Checked = true;
            }
        }

        #endregion

        #region Файл

        private void toolMenuOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         Filter = Strings.OPEN_GPX_DIALOG_FILTER,
                                         Multiselect = false
                                     };

            if(openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

             var fileStream = new StreamReader(openFileDialog.OpenFile()).ReadToEnd();


             _device.Logger.Log(string.Format("Файл {0} успешно открыт.", openFileDialog.FileName), SimpleLogger.MessageLevel.Success);

             Cursor.Current = Cursors.WaitCursor;
             
            // Отправляем на данные на сервер:
             var resp = PostRequestUtils.FileRequest.UploadFile(ConfigHelper.ExportUrl, ConfigHelper.ExportArgumentName, fileStream);

            _device.Logger.Log(string.Format("Получено {0} байт.", resp), SimpleLogger.MessageLevel.Information);

             Cursor.Current = Cursors.Arrow;

        }

        private void toolMenuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(Strings.EXIT_FROM_APP_QUESTION,
                                 Strings.EXIT_FROM_APP_CAPTION,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question,
                                 MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }

            SaveConfigure();
        }

        private void SaveConfigure()
        {
            ConfigHelper.Port = _comPort;
        }

        #endregion

        #region Help

        private void toolAbout_Click(object sender, EventArgs e)
        {
            new Dialogs.AboutBox().ShowDialog();
        }

        #endregion

        #region Port

        private void SetPortCheckedValue(string portName, bool value)
        {
            var prevComPort = menuStrip1.Items.Find(_comPort.ToLower(), true);
            var controls = menuStrip1.Items.Find(portName.ToLower(), true);

            //Снимаем отметку с предыдущего порта:
            if (prevComPort.Length > 0)
                foreach (var comPortControl in prevComPort)
                    ((ToolStripMenuItem)comPortControl).Checked = false;

            if (controls.Length == 0)
                return;

            if(controls[0] is ToolStripMenuItem)
                ((ToolStripMenuItem) controls[0]).Checked = value;
        }


        private void CheckedPort(object sender, EventArgs e)
        {
            if (_comPort == ((ToolStripMenuItem)sender).Name)
                return;

            SetPortCheckedValue(_comPort, false);

            _comPort = ((ToolStripMenuItem)sender).Name.ToUpper();

            _device.SetPort(_comPort, ConfigHelper.BaudRate);
        }

        #endregion

        private void AddItemToConsole(string text, Color foreColor)
        {
            var dateTime = DateTime.Now;

            _log.AppendFormat("<row DateTime=\"{0}\" Text=\"{1}\" />\n", dateTime.ToString("yyyy-dd-MM hh.mm.ss"), text);

            var time = string.Format("[{0}]", dateTime.ToLongTimeString());
            var item = new ListViewItem { Text = time, ForeColor = foreColor };
            item.SubItems.Add(text);

            listConsole.Items.Add(item);

            listConsole.EnsureVisible(listConsole.Items.Count - 1);
        }

        private void ContextMenuClearClick(object sender, EventArgs e)
        {
            //Очищаем:
            listConsole.Items.Clear();
        }

        #region Internet

        private bool Autorization()
        {
            var authorizationDialog = new AuthorizationDialog();

            var result = authorizationDialog.ShowDlg();
            if (result == DialogResult.Cancel || result == DialogResult.None)
                return false;

            Cursor = Cursors.WaitCursor;

            //Отправляем на данные на сервер:
            var resp = PostRequestUtils.PostRequest.Post(ConfigHelper.AuthUrl, new Dictionary<string, string>
                                                                                     {
                                                                                         {
                                                                                             ConfigHelper.AuthArgumentName,
                                                                                             authorizationDialog.
                                                                                             Password
                                                                                             }
              
                                                                                     }, null, ConfigHelper.Proxy).Split(';');
            Cursor = Cursors.Arrow;

            bool value;
            if (resp.Length > 1 &&  bool.TryParse(resp[0], out value))
            {
                _device.Logger.Log(resp[1], SimpleLogger.MessageLevel.Success );
                return value;
            }

            _device.Logger.Log("Авторизация не пройдена. Проверьте правильность пароля.", SimpleLogger.MessageLevel.Error);

            return false;
        }

        private void Ping()
        {
            Cursor = Cursors.WaitCursor;

            //Отправляем на данные на сервер:
            var resp = PostRequestUtils.PostRequest.Post(ConfigHelper.PingUrl, ConfigHelper.Proxy);

            if(resp.ToLower() == "pong")
            {
                var message = "Соединение с сервером установлено";
                toolStatusLabel.Text = message;
                _device.Logger.Log(message, SimpleLogger.MessageLevel.Success);
            }
            else
            {
                _device.Logger.Log("Не удалось соединится с сервером.", SimpleLogger.MessageLevel.Error);
            }
            
            Cursor = Cursors.Arrow;
        }

        #endregion

        private void toolRefreshPort_Click(object sender, EventArgs e)
        {
            toolStatusLabel.Text = Strings.AUTO_DETECTION_PORT;
            Cursor = Cursors.WaitCursor;
            //Обновляем статус и перерисовываем форму:
            Update();

            _device.SetPort(AutoDetectComPort(), ConfigHelper.BaudRate);

            //Отмечаем выбранный порт:
            SetPortCheckedValue(_device.PortName, true);
            _comPort = _device.PortName;

            _device.Logger.Log(string.Format("Порт: {0}", _device.PortName), SimpleLogger.MessageLevel.Information);


            Cursor = Cursors.Arrow;
            toolStatusLabel.Text = Strings.SUCCESS;
        }

        private void GlobalSatModelCheckedChanged(object sender, EventArgs e)
        {
            var baudRate = int.Parse(((RadioButton) sender).Tag.ToString());

            if (baudRate != ConfigHelper.BaudRate)
            {
                ConfigHelper.BaudRate = baudRate;
                _device.SetPort(ConfigHelper.Port, ConfigHelper.BaudRate);
                _device.SetBaudRate(ConfigHelper.BaudRate);
            }
        }

        private void cbAutoClear_CheckedChanged(object sender, EventArgs e)
        {
            ConfigHelper.AutoClear = cbAutoClear.Checked;
        }

    }
}
