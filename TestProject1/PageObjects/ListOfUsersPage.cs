using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Base;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestProject1.PageObjects
{
    public class ListOfUsersPage : BasePage
    {
        public IList<IWebElement> Rows => FindElements(By.CssSelector(".tabulator-row"));
        public IWebElement UsersTable => FindElement(By.Id("users-table"));
        public IWebElement PageHeader => FindElement(By.CssSelector("div[class='col-9'] h3]"));
        public IWebElement EmailHeader => FindElement(By.CssSelector(".tabulator-col[tabulator-field='email'"));
        public IWebElement ColumnCell(string column) => FindElement(By.CssSelector($".tabulator-cell[tabulator-field='{column}']"));

        
        public ListOfUsersPage(IWebDriver driver) : base(driver) { }

        public bool IsPageLoaded() => PageHeader.Displayed;

        public bool IsTableLoaded() => UsersTable.Displayed;

        public bool IsNewRowAdded(int rowsCount) => FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1})")).Displayed;
        
        public void WaitForTableLoaded()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(25));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("user-table")));
        }

        public void VerifyCreatedUser(int rowsCount, string email, string address1, string address2, string city, string zip, string description, string anual)
        {
            var role = "user";
            var state = " ";
            var demo = " ";
            var waitForSupervisor = " ";
            var managerType = " ";           

            Assert.AreEqual(email, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(1)")).Text, "Email are not equal");
            Assert.AreEqual(role, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(2)")).Text, "Role are not equal");
            Assert.AreEqual(address1, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(3)")).Text, "Address1 are not equal");
            Assert.AreEqual(address2, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(4)")).Text, "Address2 are not equal");
            Assert.AreEqual(city, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(5)")).Text, "City are not equal");
            Assert.AreEqual(state, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(6)")).Text, "State are not equal");
            Assert.AreEqual(zip, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(7)")).Text, "Zip are not equal");
            Assert.AreEqual(description, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(8)")).Text, "Description are not equal");
            Assert.AreEqual(demo, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(9)")).Text, "Demo are not equal");
            Assert.AreEqual(anual, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(10)")).Text, "Anual are not equal");
            Assert.AreEqual(waitForSupervisor, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(11)")).Text, "Wait for supervisor are not equal");
            Assert.AreEqual(managerType, FindElement(By.CssSelector($".tabulator-row:nth-child({rowsCount + 1}) .tabulator-cell:nth-child(12)")).Text, "Manager type are not equal");

        }
    }
}
