using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.HttpContext
{
    public class JsonContent
    {
        public static string downloadJson(string url,CookieCollection cc)
        {
            CookieContainer container = new CookieContainer();
            container.Add(cc);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            //request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("username:password"));
            //request.PreAuthenticate = true;
            request.CookieContainer = container;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                //Console.WriteLine(reader.ReadToEnd());
                return reader.ReadToEnd();
            }
            return "";
        }

        public static void saveJson(string fileName,string jsonDate)
        {

            try
            {
                var splashInfo = JsonConvert.DeserializeObject(jsonDate);
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.WriteLine(splashInfo);
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
