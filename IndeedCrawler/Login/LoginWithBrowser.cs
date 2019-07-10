using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
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
        public static CookieCollection getCookies(string url, string username, string password)
        {
            ChromeOptions options = new ChromeOptions();  
            string startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\ChromExtension";  
            
            options.AddArguments("load-extension=" + startDirectory);  
           
            string chromedriver_path= Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\";  //为什么是这个？？

            using (var driver = new ChromeDriver(chromedriver_path, options))
            {
                // Go to the home page
                driver.Navigate().GoToUrl(url);

                // Get User Name field, Password field and Login Button
                var userNameField = driver.FindElement(By.Id("login-email-input"));  //html中input的text的id
                var userPasswordField = driver.FindElement(By.Id("login-password-input"));  //html中input的password的id
                // Type user name and password
                userNameField.SendKeys(username);

                //System.Threading.Thread.Sleep();  //3-5s  c# random function ms

                userPasswordField.SendKeys(password);

                var loginButton = driver.FindElement(By.XPath("//button[@class='icl-Button icl-Button--primary icl-Button--md icl-Button--block']"));

                //System.Threading.Thread.Sleep();  //3-5s  c# random function ms
                // and click the login button
                loginButton.Click();

                CookieCollection cc = new CookieCollection();
                foreach(OpenQA.Selenium.Cookie ck in driver.Manage().Cookies.AllCookies)//Manage()：returns an IOptions object that allows the driver to set the speed and cookies
                                                                                        //     and getting cookies
                {
                    System.Net.Cookie cookie = new System.Net.Cookie();
                    cookie.Name = ck.Name;  
                    cookie.Value = ck.Value;  
                    cookie.Domain = ck.Domain;
                    cookie.Expires = ck.Expiry==null? DateTime.Now.AddDays(10): (DateTime)ck.Expiry;
                    cookie.Secure = ck.Secure;
                    cookie.HttpOnly = ck.IsHttpOnly;
                    cookie.Path = ck.Path;
                    cc.Add(cookie); //add cookie to cookieCollection
                }
                driver.Quit();
                return cc;
            }
        }
    }
}
