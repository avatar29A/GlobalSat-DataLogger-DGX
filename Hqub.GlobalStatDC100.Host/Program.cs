using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Hqub.GlobalStatDC100.Host
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NLog.Logger Log = NLog.LogManager.GetLogger("isupervise");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += (s, arg) => Log.Error("UnhandledException.", arg.ExceptionObject.ToString());

            try
            {
                Application.Run(new MainForm());
                Application.ThreadException += (sender, e) => Log.ErrorException("ThreadException.",e.Exception);

            }
            catch (Exception exception)
            {
                Log.ErrorException(Strings.FatalError, exception);
            }
        }
    }
}
