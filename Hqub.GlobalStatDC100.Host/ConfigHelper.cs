using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace Hqub.GlobalStatDC100.Host
{
    public class ConfigHelper
    {
        private const string DefaultPort = "COM17";
        private const int DefaultBaudRate = 115200;
        private const string DefaultUrl = "http://localhost";
        private const string DefaultArgumentName = "ArgumentName";

        private const string Path = "config.xml";

        public static string Port
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultPort;

                var device = doc.GetElementsByTagName("device").Item(0);

                if (device == null)
                    return DefaultPort;

                var port = device.Attributes["Port"];

                return port == null ? DefaultPort : port.Value;
            }
            set
            {
                var doc = OpenConfig();
                if(doc == null)
                    return;

                var device = doc.GetElementsByTagName("device").Item(0);

                if (device == null)
                    return;

                device.Attributes["Port"].Value = value;

                doc.Save(Path);
            }
        }

        public static int BaudRate
        {
            get
            {

                var doc = OpenConfig();
                if (doc == null)
                    return DefaultBaudRate;

                var device = doc.GetElementsByTagName("device").Item(0);

                if (device == null)
                    return DefaultBaudRate;

                return int.Parse(device.Attributes["BaudRate"].Value);
            }

            set
            {
                var doc = OpenConfig();
                if (doc == null)
                    return;

                var device = doc.GetElementsByTagName("device").Item(0);

                if (device == null)
                    return;

                device.Attributes["BaudRate"].Value = value.ToString();

                doc.Save(Path);
            }
        }

        public static string ExportUrl
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("export").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["Url"].Value;
            }
        }

        public static string AuthUrl
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("auth").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["Url"].Value;
            }
        }

        public static string PingUrl
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("ping").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["Url"].Value;
            }
        }

        public static string LogUrl
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("log").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["Url"].Value;
            }
        }

        public static string LogArgumentName
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("log").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["ArgumentName"].Value;
            }

        }

        public static string ExportArgumentName
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("export").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["ArgumentName"].Value;
            }
        }

        public static string AuthArgumentName
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return DefaultUrl;

                var export = doc.GetElementsByTagName("auth").Item(0);

                if (export == null)
                    return DefaultUrl;

                return export.Attributes["ArgumentName"].Value;
            }
        }

        public static bool AutoClear
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return true;

                var app = doc.GetElementsByTagName("app").Item(0);

                if (app == null)
                    return true;

                return bool.Parse(app.Attributes["AutoClear"].Value);
            }

            set
            {
                var doc = OpenConfig();
                if (doc == null)
                    return;

                var app = doc.GetElementsByTagName("app").Item(0);

                if (app == null)
                    return;

                app.Attributes["AutoClear"].Value = value.ToString();

                doc.Save(Path);
            }
        }

        public static int MaxPorts
        {
            get
            {
                var doc = OpenConfig();
                if (doc == null)
                    return 9;

                var app = doc.GetElementsByTagName("app").Item(0);

                return app == null ? 9 : int.Parse(app.Attributes["MaxPorts"].Value);
            }

            set
            {
                var doc = OpenConfig();
                if (doc == null)
                    return;

                var app = doc.GetElementsByTagName("app").Item(0);

                if (app == null)
                    return;

                app.Attributes["MaxPorts"].Value = value.ToString();

                doc.Save(Path);
            }
        }

        public static WebProxy Proxy 
        {
            get
            {
                try
                {
                    var doc = OpenConfig();

                    var nodeProxy = doc.GetElementsByTagName("proxy").Item(0);

                    var proxy = new WebProxy(nodeProxy["address"].InnerText, int.Parse(nodeProxy["port"].InnerText));

                    var isUse = bool.Parse(nodeProxy["is_use"].InnerText);
                    if(!isUse)
                        return null;

                    var isAuth = bool.Parse(nodeProxy["is_auth"].InnerText);

                    if(isAuth)
                    {
                        var username = nodeProxy["username"].InnerText;
                        var password = nodeProxy["password"].InnerText;

                        proxy.Credentials = new NetworkCredential(username, password);
                    }
                 
                    return proxy;
                }catch(Exception)
                {
                    return null;
                }
            }
        }

        private static XmlDocument OpenConfig()
        {
            if (!System.IO.File.Exists(Path))
            {
                System.Windows.Forms.MessageBox.Show(Strings.CONFIG_NOT_FOUND);
                return null;
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(System.IO.File.ReadAllText(Path));

            return xmlDocument;
        }
    }
}
