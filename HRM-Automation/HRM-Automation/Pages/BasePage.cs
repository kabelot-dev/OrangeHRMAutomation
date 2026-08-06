using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM_Automation.Pages
{
    internal class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver myDriver)
        {
            this.Driver = myDriver;
        }
    }
}
