using NUnit.Framework;
using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CodilityTest.Configs
{
    public static class TestSetup
    {
        public static IWebDriver _driver = null;
        public static string _browser = ((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("TestConfigs"))).Get("Browser");
        public static string _URL = ((System.Collections.Specialized.NameValueCollection)(ConfigurationManager.GetSection("TestConfigs"))).Get("DemoShopURL");
        //public static string _reportPath = "Reports&Results_" + DateTime.Now.ToString("MM_dd_yyyy") + "&" + TestContext.CurrentContext.Test.Name + DateTime.Now.ToString("_HH_mm_ss");

        /// <summary>
        /// Method to setup and launch driver
        /// </summary>
        /// <returns>WebDriver</returns>
        public static IWebDriver Launch()
        {
            try
            {
                if (_browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
                {
                    var DeviceDriver = ChromeDriverService.CreateDefaultService();
                    DeviceDriver.HideCommandPromptWindow = true;
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-infobars");
                    _driver = new ChromeDriver(DeviceDriver, options);
                    _driver.Manage().Window.Maximize();
                    _driver.Navigate().GoToUrl(_URL);
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    TestContext.Out.WriteLine("[INFO]:Browser [" + _browser + "] launched with URL [" + _URL + "]");
                }
                if (_browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
                { throw new NotImplementedException("Not implemented"); }
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine(ex.Message);
            }
            return _driver;
        }

        //public static DefaultWait<IWebDriver> Wait()
        //{
        //    DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_driver);
        //    fluentWait.Timeout = TimeSpan.FromSeconds(5);
        //    fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
        //    fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        //    fluentWait.Message = "Element to be searched not found";
        //    return fluentWait;
        //}
        //public static string GetPath()
        //{
        //    try
        //    {
        //        var path = Assembly.GetCallingAssembly().CodeBase;
        //        var actualPath = path.Substring(0, path.LastIndexOf(Assembly.GetCallingAssembly().FullName.Split(',')[0]));
        //        var projectPath = new Uri(actualPath).LocalPath;
        //        return projectPath;
        //    }
        //    catch (Exception e) { Console.WriteLine(e.Message); return null; }
        //}
    }
}
