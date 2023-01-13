using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TestProject1.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Base
{
    public class BaseClass
    {
        public IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["url"]);
            driver.Manage().Window.Maximize();            

        }
        
        public Dictionary<string, string> userData;



        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
