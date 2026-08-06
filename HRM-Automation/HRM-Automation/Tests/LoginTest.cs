using HRM_Automation.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM_Automation.Tests
{
    [TestFixture]
    internal class LoginTest : BaseTest
    {
        [SetUp]
        public void init() { 

        setup("chrome", "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
        }

        [TestCase("Admin", "admin123", "dashboard", TestName = "Valid Login Test",
            Description = "Verify that user can login with valid credentials",
            Author = "Kabelo Tlhape")]
        public void LoginTestWithParameters(string username, string password, string expectedText)
        {
            Login_Page?.Login(username, password);
            bool expected = Driver.Url.Contains(expectedText);
            Assert.That(expected, $"Url should contain '{expectedText}'");
        }

        [Test]
        [Description("Verify login with invalid password")]
        public void InvalidPasswordLoginTest()
        {
            string expectedErrorMessage = "Invalid credentials";
            Login_Page?.Login("Admin", "Password123");
            string? actualErrorMessage = Login_Page?.GetErrorMessage();
            Assert.That(expectedErrorMessage, Is.EqualTo(actualErrorMessage), "Error message should match expected value");
        }

        [Test]
        [Description("Verify login using an invalid username")]
        public void InvalidUsernameLoginTest()
        {
            string expectedErrorMessage = "Invalid credentials";
            Login_Page?.Login("TestUser", "admin123");
            string? actualErrorMessage = Login_Page?.GetErrorMessage();
            Assert.That(expectedErrorMessage, Is.EqualTo(actualErrorMessage), "Error message should match expected value");
        }

        [TestCase("", "admin123", "Required", TestName = "Empty Username Test",
            Description = "Verify that user cannot login with empty username",
            Author = "Kabelo Tlhape")]
        public void EmptyUsernameLoginTest(string username, string password, string expectedErrorMessage)
        {
            Login_Page?.Login(username, password);
            string? actualErrorMessage = Login_Page?.GetRequiredFieldUsernameErrorMessage();
            Assert.That(expectedErrorMessage, Is.EqualTo(actualErrorMessage), "Error message should match expected value");
        }

        [TestCase("Admin", "", "Required", TestName = "Empty Password Test",
        Description = "Verify that user cannot login with empty password",
        Author = "Kabelo Tlhape")]
        public void EmptyPasswordLoginTest(string username, string password, string expectedErrorMessage)
        {
            Login_Page?.Login(username, password);
            string? actualErrorMessage = Login_Page?.GetRequiredFieldPasswordErrorMessage();
            Assert.That(expectedErrorMessage, Is.EqualTo(actualErrorMessage), "Error message should match expected value");
        }

        [Test(Description = "Verify Forgot Password navigation", Author = "Kabelo Tlhape")]
        public void ForgotPasswordLinkTest()
        {
            string expectedUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/requestPasswordResetCode";
            string? actualUrl = Login_Page?.OpenForgotPasswordPage();
            Assert.That(expectedUrl, Is.EqualTo(actualUrl), "Forgot Password page URL should match expected value");
        }

       





    }
}
