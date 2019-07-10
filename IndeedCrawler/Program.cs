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
                string city = "Calgary";
                string province = "AB";

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
            CookieCollection cc = LoginWithBrowser.getCookies("https://secure.indeed.com/account/login"
                , "futong101@gmail.com"
                , "Tt10011996");
            clientToken = cc["CTK"].Value;
            System.Threading.Thread.Sleep(2000);

            foreach (string kd in searchDeveloper)
            {
                string keyword = kd.Replace(" ", "+");
                int pageCount = 0;
                while (true)
                {
                    RedirectWithCookies rwc = new Redirect.RedirectWithCookies(cc); 
                    //HtmlDocument htmlDoc = rwc.getPageWithCookie("https://www.indeed.com/resumes?q=" + keyword + "&l=" + city + "%2C+" + province + "&start=" + pageCount.ToString());
                    
                    //HtmlDocument： get HTML document from HTTP
                    HtmlDocument htmlDoc = rwc.getPageWithCookieAsync("https://resumes.indeed.com/search?q=" + keyword + "&l=" + city + "%2C+" + province + "&start=" + pageCount.ToString());
                    if (htmlDoc == null)
                        break;
                    //string xpath = "//ol[@id='results']/li";
                    string xpath = "//*[@class='rezemp-ResumeSearchCard-contents']/div/span[1]";
                    
                    string clientIds = HttpContext.ExtractClientIds.getClientIds(htmlDoc, xpath);
                    if (clientIds == null)
                        break;
                    
                    string sendUrl = domainPreHttp + clientIds + "&q=" + keyword + "&tk=" + clientToken;
                    var result = JsonContent.downloadJson(sendUrl, cc);
                    string filename = @"E:\IndeedFile\" + city + "_" + province + "_" + keyword + "_" + pageCount.ToString() + ".json";
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
