using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM_Automation.Pages
{
    internal class LoginPage : BasePage
    {
 
        private IWebElement TxtUsername => Driver.FindElement(By.CssSelector("input[placeholder='Username']"));
        private IWebElement TxtPassword => Driver.FindElement(By.CssSelector("input[placeholder='Password']"));
        private IWebElement BtnLogin => Driver.FindElement(By.CssSelector("button[type='submit']"));
        private IWebElement LinkForgotPass => Driver.FindElement(By.CssSelector(".oxd-text.oxd-text--p.orangehrm-login-forgot-header"));




        public LoginPage(IWebDriver myDriver) : base(myDriver)  
        {
            WaitForLoginPageToLoad();
        }

        public void Login(string username, string password)
        {
            TxtUsername.SendKeys(username); 
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }

        public string GetErrorMessage()
        {

            WebElement lblErrorMsg = WaitForElementToBeVisible(".oxd-text.oxd-text--p.oxd-alert-content-text");

            return lblErrorMsg.Text;
        }

        public void WaitForLoginPageToLoad()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.Until(driver => TxtUsername.Displayed && TxtPassword.Displayed && BtnLogin.Displayed);
        }

        public string OpenForgotPasswordPage()
        {
            LinkForgotPass.Click();

            return Driver.Url;
        }

        public WebElement WaitForElementToBeVisible(string locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(300),
            };

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            WebElement element = (WebElement)wait.Until(d =>
            {
                return d.FindElement(By.CssSelector(locator));
            });

            return element;

        }

        public string GetRequiredFieldUsernameErrorMessage()
        {
            return WaitForElementToBeVisible("body > div:nth-child(3) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2) > div:nth-child(3) > form:nth-child(2) > div:nth-child(2) > div:nth-child(1) > span:nth-child(3)").Text;
        }

        public string GetRequiredFieldPasswordErrorMessage()
        {
            return WaitForElementToBeVisible("body > div:nth-child(3) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(2) > div:nth-child(3) > form:nth-child(2) > div:nth-child(3) > div:nth-child(1) > span:nth-child(3)").Text;
        }

    }
}
