using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Hqub.PostRequestUtils
{
    public static class FileRequest
    {
        public static string UploadFile(string url, string argumentName, string content)
        {

            long length = 0;
            var boundary = "----------------------------" +
                              DateTime.Now.Ticks.ToString("x");


            var httpWebRequest2 = (HttpWebRequest) WebRequest.Create(url);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" +
                                          boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream memStream = new MemoryStream();

            var boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

//            var formdataTemplate = "\r\n--" + boundary +
//                                      "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
//
//            foreach (string key in nvc.Keys)
//            {
//                string formitem = string.Format(formdataTemplate, key, nvc[key]);
//                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
//                memStream.Write(formitembytes, 0, formitembytes.Length);
//            }

            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            const string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

            //string header = string.Format(headerTemplate, "file" + i, files[i]);
            var header = string.Format(headerTemplate, argumentName, "export.gpx");

            var headerbytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(headerbytes, 0, headerbytes.Length);

            var fileStream = new MemoryStream(Encoding.Default.GetBytes(content));

            var buffer = new byte[1024];
            var bytesRead = 0;


            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);

            }

            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            fileStream.Close();

            httpWebRequest2.ContentLength = memStream.Length;

            var requestStream = httpWebRequest2.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            var webResponse2 = httpWebRequest2.GetResponse();

            var stream2 = webResponse2.GetResponseStream();
            var reader2 = new StreamReader(stream2, Encoding.Default);
            var response = reader2.ReadToEnd();

            webResponse2.Close();

            return response;
        }
    
//        public static int UploadData(string url, string content, string argument)
//        {
//			using (var client = new WebClient())
//			{
//				client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
//			    var r = client.UploadString(url, "POST", string.Format("{0}={1}", argument, System.Uri.EscapeDataString(content)));
//
//				return r.Length;
//			}
//        }
    }
}
