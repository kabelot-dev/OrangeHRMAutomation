using HRM_Automation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM_Automation.Tests
{
    internal class BaseTest
    {
        protected IWebDriver? Driver { get; set; }
        protected LoginPage? Login_Page { get; set; }

        public void setup(string browser, string url)
        {
            Driver = InitializeWebDriver(browser, url);
            Login_Page = new LoginPage(Driver);
        }

        public IWebDriver InitializeWebDriver(string browser, string url)
        {
            if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                Driver = new ChromeDriver();
            }
            else if (browser.Equals("edge", StringComparison.OrdinalIgnoreCase))
            {
                Driver = new EdgeDriver();
            }
            else
            {
                throw new ArgumentException("Unsupported browser: " + browser);
            }

            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();

            return Driver;
        }

        public void teardown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }
        }
    }
}
