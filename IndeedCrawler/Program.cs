using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IndeedCrawler.Login;
using HtmlAgilityPack;
using IndeedCrawler.Redirect;
using IndeedCrawler.HttpContext;

namespace IndeedCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> searchDeveloper = new List<string>() {
                    "DBA"
                };
                string domainPreHttp = "https://www.indeed.com/resumes/rpc/preview?keys=";
                string city = "Toronto";
                string province = "ON";

                loadPageByBrowser(searchDeveloper, domainPreHttp, city, province);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void loadPageByBrowser(List<string> searchDeveloper, string domainPreHttp, string city, string province)
        {
            string clientToken;
            //CookieCollection cc = LoginWithBrowser.getCookies("https://secure.indeed.com/account/login"
            //    , "13121348518@163.com"
            //    , "Tt10011996");
            //clientToken = cc["CTK"].Value;
            //System.Threading.Thread.Sleep(2000);

            foreach (string kd in searchDeveloper)
            { 
                string keyword = kd.Replace(" ", "+");
                int pageCount = 0;
                while (true)
                {

                    CookieCollection cc = LoginWithBrowser.getCookiesInResumePage(
                        "https://secure.indeed.com/account/login"
                        , "ttmm.li@hotmail.com"
                        , "Tt10011996"
                        , "https://resumes.indeed.com"
                        , kd
                        , city + " " + province);


                    clientToken = cc["CTK"].Value;
                    RedirectWithCookies rwc = new Redirect.RedirectWithCookies(cc);

                    //string Url = "https://resumes.indeed.com/rpc/search?hasScore=1&l=" + city + "&q=" + keyword + "&searchFields=jt";

                    //HtmlDocument htmlDoc = rwc.getPageWithCookieAsync("https://resumes.indeed.com/search?q=" + keyword + "&l=" + city + "%2C+" + province + "&start=" + pageCount.ToString());

                    string Url = "https://resumes.indeed.com/search?q="+kd+"&l="+city+"+"+province+"&searchFields=jt";
                    HtmlDocument htmlDoc = rwc.getPageWithCookieAsync(Url);
                    if (htmlDoc == null)
                        break;
                    string xpath = "//*[@id='content']/div/div[2]/div/div[2]/div[1]/div[3]/div[1]/div[1]/div[1]/div";
                    string clientIds = HttpContext.ExtractClientIds.getClientIds(htmlDoc, xpath);
                    if (clientIds == null)
                        break;
                    string sendUrl = domainPreHttp + clientIds + "&q=" + keyword + "&tk=" + clientToken;


                    //string sendUrl = "https://resumes.indeed.com/rpc/search?hasScore=1&l=toronto&q=dba&searchFields=jt";

                    var result = JsonContent.downloadJson(sendUrl, cc);
                    string filename = @"D:\IndeedFile\" + city + "_" + province + "_" + keyword + "_" + pageCount.ToString() + ".json";
                    JsonContent.saveJson(filename, result);
                    pageCount = pageCount + 50;
                    System.Threading.Thread.Sleep(2000);
                }
                System.Threading.Thread.Sleep(2000);
            }
        }

        private static void loadPageByHttpRequest(List<string> searchDeveloper, string domainPreHttp, string city, string province)
        {
            string clientToken = "";
            HttpWebResponse response = HttpRequestLogin.returnHttpResponseWithCookie(
                "https://secure.indeed.com/account/login"
                , "jwang4@ualberta.ca"
                , "ii62399484indeed",
                true, "submit", ref clientToken);

            //https://www.indeed.com/resumes?q=developer&l=Edmonton%2C+AB&co=CA&start=50
            foreach (string kd in searchDeveloper)
            {
                string keyword = kd.Replace(" ", "+");
                int pageCount = 0;
                while (true)
                {
                    RedirectWithCookies rwc = new Redirect.RedirectWithCookies(response.Cookies);
                    HtmlDocument htmlDoc = rwc.getPageWithCookieAsync("https://www.indeed.com/resumes?q=" + keyword + "&l=" + city + "%2C+" + province + "&start=" + pageCount.ToString());
                    if (htmlDoc == null)
                        break;
                    string xpath = "//*[@id='content']/div/div[2]/div/div[2]/div[2]/div[2]/div/div[1]/span[1]";
                    string clientIds = HttpContext.ExtractClientIds.getClientIds(htmlDoc, xpath);
                    string sendUrl = domainPreHttp + clientIds + "&q=" + keyword + "&tk=" + clientToken;
                    var result = JsonContent.downloadJson(sendUrl, response.Cookies);
                    string filename = @"E:\IndeedFile\" + city + "_" + province + "_" + keyword + "_" + pageCount.ToString() + ".json";
                    JsonContent.saveJson(filename, result);
                    pageCount = pageCount + 50;
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }
    }
}
