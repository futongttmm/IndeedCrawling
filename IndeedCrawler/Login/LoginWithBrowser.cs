using IndeedCrawler.Redirect;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.Login
{
    public class LoginWithBrowser
    {
        //public static CookieCollection getCookies(string url, string username, string password)
        //{
        //    ChromeOptions options = new ChromeOptions();
        //    string startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\ChromExtension";
        //    options.AddArguments("load-extension=" + startDirectory);

        //    string chromedriver_path= Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\";

        //    using (var driver = new ChromeDriver(chromedriver_path, options))
        ////    {
        //// Go to the home page
        //driver.Navigate().GoToUrl(url);

        //// Get User Name field, Password field and Login Button
        //var userNameField = driver.FindElement(By.Id("login-email-input"));
        //var userPasswordField = driver.FindElement(By.Id("login-password-input"));
        //// Type user name and password
        //userNameField.SendKeys(username);

        //        System.Threading.Thread.Sleep(new Random().Next(3, 5) * 1000);

        //        userPasswordField.SendKeys(password);

        //        System.Threading.Thread.Sleep(new Random().Next(2, 3) * 1000);

        //        var loginButton = driver.FindElement(By.XPath("//button[@class='icl-Button icl-Button--primary icl-Button--md icl-Button--block']"));

        //// and click the login button
        //loginButton.Click();

        //        CookieCollection cc = new CookieCollection();
        //        foreach(OpenQA.Selenium.Cookie ck in driver.Manage().Cookies.AllCookies)
        //        {
        //            System.Net.Cookie cookie = new System.Net.Cookie();
        //            cookie.Name = ck.Name;
        //            cookie.Value = ck.Value;
        //            cookie.Domain = ck.Domain;
        //            cookie.Expires = ck.Expiry==null? DateTime.Now.AddDays(10): (DateTime)ck.Expiry;
        //            cookie.Secure = ck.Secure;
        //            cookie.HttpOnly = ck.IsHttpOnly;
        //            cookie.Path = ck.Path;

        //            Console.WriteLine("\t" + cookie.ToString());


        //            cc.Add(cookie);
        //        }

        //        driver.Quit();
        //        return cc;
        //    }
        //}


        public static CookieCollection getCookiesInResumePage(string url, string username, string password, string secondUrl, string searchDeveloper, string destination)
        {
            ChromeOptions options = new ChromeOptions();
            string startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\ChromExtension";
            options.AddArguments("load-extension=" + startDirectory);

            string chromedriver_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\";

            using (var driver = new ChromeDriver(chromedriver_path, options))
            {

                // Go to the home page
                driver.Navigate().GoToUrl(url);

                // Get User Name field, Password field and Login Button
                var userNameField = driver.FindElement(By.Id("login-email-input"));
                var userPasswordField = driver.FindElement(By.Id("login-password-input"));
                // Type user name and password
                userNameField.SendKeys(username);

                System.Threading.Thread.Sleep(new Random().Next(3, 5) * 1000);

                userPasswordField.SendKeys(password);

                System.Threading.Thread.Sleep(new Random().Next(2, 3) * 1000);

                var loginButton = driver.FindElement(By.XPath("//button[@class='icl-Button icl-Button--primary icl-Button--md icl-Button--block']"));

                // and click the login button
                loginButton.Click();

                CookieCollection cc = new CookieCollection();
                foreach (OpenQA.Selenium.Cookie ck in driver.Manage().Cookies.AllCookies)
                {
                    System.Net.Cookie cookie = new System.Net.Cookie();
                    cookie.Name = ck.Name;
                    cookie.Value = ck.Value;
                    cookie.Domain = ck.Domain;
                    cookie.Expires = ck.Expiry == null ? DateTime.Now.AddDays(10) : (DateTime)ck.Expiry;
                    cookie.Secure = ck.Secure;
                    cookie.HttpOnly = ck.IsHttpOnly;
                    cookie.Path = ck.Path;

                    cc.Add(cookie);

                    driver.Manage().Cookies.AddCookie(ck);
                }


                System.Threading.Thread.Sleep(6000);

                // Go to the resume home page
                driver.Navigate().GoToUrl(secondUrl);
                var searchField = driver.FindElement(By.Id("input-q"));
                var destinationField = driver.FindElement(By.Id("input-l"));

                searchField.SendKeys(searchDeveloper);

                System.Threading.Thread.Sleep(new Random().Next(3, 5) * 1000);

                destinationField.SendKeys(destination);

                System.Threading.Thread.Sleep(new Random().Next(2, 3) * 1000);

                var searchButton = driver.FindElement(By.XPath("//*[@id='content']/div/div[2]/div/div[1]/div/div[2]/div/form/div[3]/button"));

                // and click the login button
                searchButton.Click();

                foreach (OpenQA.Selenium.Cookie ck in driver.Manage().Cookies.AllCookies)
                {
                    System.Net.Cookie cookie = new System.Net.Cookie();
                    cookie.Name = ck.Name;
                    cookie.Value = ck.Value;
                    cookie.Domain = ck.Domain;
                    cookie.Expires = ck.Expiry == null ? DateTime.Now.AddDays(10) : (DateTime)ck.Expiry;
                    cookie.Secure = ck.Secure;
                    cookie.HttpOnly = ck.IsHttpOnly;
                    cookie.Path = ck.Path;

                    Console.WriteLine("\t" + cookie.ToString());

                    cc.Add(cookie);
                }

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(
                    By.ClassName("icl-Grid-col icl-u-xs-span12 icl-u-md-span8 rezemp-ResumeSearchPage-resumeList")));

                driver.Quit();
                return cc;
            }

        }
    }
}
