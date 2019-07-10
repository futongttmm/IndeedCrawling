using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace IndeedCrawler.Redirect
{
    public class RedirectWithCookies
    {
        public CookieContainer cookiePot;

        public RedirectWithCookies(CookieCollection cc)
        {
            CookieContainer container = new CookieContainer();
            container.Add(cc);
            cookiePot = container;
        }

        public void AddMoreCookies(CookieCollection cc)
        {
            cookiePot.Add(cc);
        }
        public HtmlDocument getPageWithCookieAsync(string redirectUrl)
        {
            try
            {
                //using (var client = new HttpClient())
                //{
                //    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.80 Safari/537.36");
                //    string html = client.GetStringAsync(redirectUrl).Result;
                //    var web = new HtmlAgilityPack.HtmlDocument();
                //    //web.UseCookies = true;
                //    //web.PreRequest = new HtmlWeb.PreRequestHandler(OnPreRequest);
                //    web.LoadHtml(redirectUrl);
                //    //if (web.StatusCode == HttpStatusCode.NotFound)
                //    //{
                //    //    return null;
                //    //}
                //    return web;
                //}

                HtmlWeb web = new HtmlWeb();
                web.UseCookies = true;
                web.PreRequest = new HtmlWeb.PreRequestHandler(OnPreRequest);
                HtmlDocument htmlDoc = web.Load(redirectUrl);
                if (web.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                return htmlDoc;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //HttpWebRequest httpRequest;
            //CookieContainer container = new CookieContainer();
            //container.Add(cc);
            //// Probably want to inspect the http.Headers here first
            //httpRequest = WebRequest.Create(redirectUrl) as HttpWebRequest;
            //httpRequest.Method = "GET";
            ////httpRequest.CookieContainer = container;
            //HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse;
            //if (httpResponse.StatusCode == HttpStatusCode.OK)
            //{
            //    HtmlDocument htmlDoc;
            //    using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        var reader = sr.ReadToEnd();
            //        htmlDoc = new HtmlAgilityPack.HtmlDocument();
            //        //htmlDoc.OptionFixNestedTags = true;
            //        htmlDoc.Load(reader);
            //    }
            //    httpResponse.Close();
            //    return htmlDoc;
            //}
            //else
            //    return null;
        }
        private bool OnPreRequest(HttpWebRequest request)
        {
            request.CookieContainer = cookiePot;
            return true;
        }
    }
}
