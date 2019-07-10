using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.Login
{
    public class HttpRequestLogin
    {
        public static HttpWebResponse returnHttpResponseWithCookie(string url, string username, string password, bool keepSignedIn,string signin, ref string clientToken)
        {
            CookieContainer cc = new CookieContainer();
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.KeepAlive = true;
            httpRequest.Method = "POST";
            httpRequest.Host = "secure.indeed.com";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.Headers["Origin"]="https://secure.indeed.com";
            httpRequest.Headers["Upgrade-Insecure-Requests"]="1";
            httpRequest.Headers["Accept-Language"] = "en-US,en;q=0.8";
            httpRequest.AutomaticDecompression = DecompressionMethods.GZip;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.CookieContainer = cc;

            string postData = "signin_email=" + username + "&signin_password=" + password+ "&signin_remember="+ (keepSignedIn?"on":"off")+ "&"+ signin + "=button";
            byte[] dataBytes = UTF8Encoding.UTF8.GetBytes(postData);
            httpRequest.ContentLength = dataBytes.Length;
            using (Stream postStream = httpRequest.GetRequestStream())
            {
                postStream.Write(dataBytes, 0, dataBytes.Length);
            }
            HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;
            
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                //httpResponse.Cookies["SHOE"]= ""XY894EjZ2aDUTxDT_5c1mns_NfwSqZgjevuvPND2bPdt7V6eq31KhpIpaUd - T8WUAylMwoxao1byN7o2hBnNM2VsoUHcf3cK3qjfyMnHYAJOGZYFjmjhLxLah35x_ig = "; Domain=.indeed.com; Expires=Mon, 07-May-2018 13:57:40 GMT; Path=/; HttpOnly";
                if (httpResponse.Cookies["CTK"] != null)
                {
                    clientToken = httpResponse.Cookies["CTK"].Value;
                }
                else
                    throw new Exception(Common.ExeptionInfo.TokenHasNotReturned);
                return httpResponse;
            }
            else
                throw new Exception(Common.ExeptionInfo.CouldNotLogin);

        }
    }
}
