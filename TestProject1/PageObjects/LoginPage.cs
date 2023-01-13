using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Base;

namespace TestProject1.PageObjects
{
    public class LoginPage : BasePage
    {
        public IWebElement LoginField => FindElement(By.Id("login"));
        public IWebElement PasswordField => FindElement(By.Id("password"));
        public IWebElement BtnLogin => FindElement(By.CssSelector(".btn-primary.rounded-2"));


        public LoginPage(IWebDriver driver) : base(driver) { }


        public void Login(string login, string password)
        {
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            BtnLogin.Click();
        }
    }
}
