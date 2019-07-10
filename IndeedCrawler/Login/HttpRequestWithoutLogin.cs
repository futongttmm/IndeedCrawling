using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.Login
{
    public class HttpRequestWithoutLogin
    {
        public static string returnHttpResponse(string url)
        {
            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
            // create a request
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(url);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.ASCII.GetBytes("");

            // this is important - make sure you specify type this way
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return "StatusCode:"+response.StatusCode+"\\n\\r"+(new StreamReader(response.GetResponseStream()).ReadToEnd().ToString());
        }
    }
}
