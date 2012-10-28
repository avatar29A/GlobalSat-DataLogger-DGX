using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hqub.PostRequestUtils
{
    public class PostRequest
    {
        private static Dictionary<string,string > _postArguments = new Dictionary<string, string>();

        public static string Post(string url, WebProxy proxy)
        {
            return Post(url, _postArguments, null, proxy);
        }

        public static string Post(string url, Dictionary<string, string> postArgs, Dictionary<string,string > cookies = null, WebProxy webProxy = null)
        {
            _postArguments = postArgs;

            if (string.IsNullOrEmpty(url))
            {
                return "Url-не может быть пустым";
            }

            //Создаем новый запрос для передачи параметров:
            var request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Proxy = webProxy;

            if(cookies != null && cookies.Count > 0)
            {
                //Устанавливаем cookies
                var cookieContainer = new CookieContainer();
                foreach (var cookie in cookies)
                {
                    var c = new Cookie(cookie.Key, cookie.Value) {Domain = new Uri(url).Host};

                    cookieContainer.Add(c);
                }
                
                ((HttpWebRequest)request).CookieContainer = cookieContainer;
            }
            

            var normalizePostArgs = NormailizePost(_postArguments);
            request.ContentLength = normalizePostArgs.Length;
            
            try
            {
                var streamRequest = request.GetRequestStream();

                streamRequest.Write(normalizePostArgs, 0, normalizePostArgs.Length);

                streamRequest.Close();

                var objResponse = request.GetResponse();
                if (objResponse != null)
                {
                    var sr = new System.IO.StreamReader(objResponse.GetResponseStream(),
                                                        Encoding.UTF8);
                    var response = sr.ReadToEnd().Trim();

                    objResponse.Close();
                    sr.Close();
                    return response;
                }

                return "Сервер вернул пустой ответ";
            }
            catch (TimeoutException)
            {
                return "Превышено время ожидания ответа сервера";

            }
            catch (Exception)
            {
                return "Проверьте соединение с Интернетом";
            }
        }

        private static byte[] NormailizePost(Dictionary<string, string> post_args)
        {
            string post_string = string.Empty;
            foreach (var arg in post_args)
                post_string += string.Format("{0}={1}&", arg.Key, arg.Value);
            post_string += "fake=fake";

            //Logs_WriteXML("Список параметров отправленных на сервер", post_string);

            return Encoding.Default.GetBytes(post_string);
        }
    }
}
