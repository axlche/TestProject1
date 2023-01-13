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
    public class CoursePage : BasePage
    {
       
        #region FirstNavMenu
        public IWebElement FirstNavMenu => FindElement(By.Id("first-nav-block"));
        public IWebElement DashboardMenuItem => FindElement(By.XPath("//a[normalize-space()='Dashboard']"));
        public IWebElement OrdersMenuItem => FindElement(By.XPath("//a[normalize-space()='Orders']"));
        public IWebElement ProductsMenuItem => FindElement(By.XPath("//a[normalize-space()='Products']"));
        public IWebElement CustomersMenuItem => FindElement(By.XPath("//a[normalize-space()='Customers']"));
        public IWebElement ReportsMenuItem => FindElement(By.XPath("//a[normalize-space()='Reports']"));
        public IWebElement IntegrationsMenuItem => FindElement(By.XPath("//a[normalize-space()='Integrations']"));
        public IWebElement CrtUsersMenuItem => FindElement(By.XPath("//a[normalize-space()='Create User']"));
        public IWebElement CrtManagerMenuItem => FindElement(By.XPath("//a[normalize-space()='Create Manager']"));
        public IWebElement CrtSubsMenuItem => FindElement(By.XPath("//a[normalize-space()='Create Subscription']"));
        public IWebElement LstOfUsersMenuItem => FindElement(By.XPath("//a[normalize-space()='List of users']"));
        public IWebElement LstOfSubsMenuItem => FindElement(By.XPath("//a[normalize-space()='List of Subscriptions']"));
        #endregion
        public IWebElement LblUser => FindElement(By.Id("user-label"));
        public IWebElement BtnCheckStatus => FindElement(By.CssSelector("li.nav-item a.nav-link#status"));
        public IWebElement IdHeader => FindElement(By.CssSelector(".tabulator-col[tabulator-field='id']"));
        public IWebElement NameHeader => FindElement(By.CssSelector(".tabulator-col[tabulator-field='name']"));
        public IWebElement AgeHeader => FindElement(By.CssSelector(".tabulator-col[tabulator-field='age']"));
        public IList<IWebElement> Cells(string column)
        {
            return driver.FindElements(By.CssSelector($".tabulator-cell[tabulator-field='{column}']"));
        }

        public CoursePage(IWebDriver driver) : base(driver) { }

        public void VerifyLoggedIn(string userName)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.TextToBePresentInElement(LblUser, userName));
        }

        public void WaitFirstNavMenuLoaded()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.Id("first-nav-block")));

        }

        public void VerifyColorNotRed(IWebElement element)
        {
            Actions action = new(driver);
            action.MoveToElement(element).Perform();
            Assert.IsTrue(element.GetCssValue("color") != "rgba(255,0,0,1)", "Color is red!");
        }

        public void WaitForSpinner()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("spinner")));
        }

        public void TableSorting(string column, string sortType)
        {
            IList<IWebElement> cells = driver.FindElements(By.CssSelector($".tabulator-cell[tabulator-field='{column}']"));
            IList<string> cellValues = new List<string>();
            IList<int> cellNums = new List<int>();
            foreach (IWebElement cell in cells)
            {
                if (cell.Text.Any(char.IsDigit) && int.TryParse(cell.Text, out int value))
                    cellNums.Add(value);
                else
                    cellValues.Add(cell.Text);
            }
            if (cellValues.Count > 0)
                VerifySorting(cellValues, sortType);
            else
                VerifyNumSorting(cellNums, sortType);
        }

        public void VerifySorting(IList<string> list, string sortType)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                string currentValue = list[i];
                string nextValue = list[i + 1];
                CompareSortedValues(i, currentValue, nextValue, sortType);
            }
        }

        public void VerifyNumSorting(IList<int> list, string sortType)
        {
            if (sortType == "asc")
                Assert.IsTrue(list.SequenceEqual(list.OrderBy(x => x)), "Column not sorted in ascending order");
            else
                Assert.IsTrue(list.SequenceEqual(list.OrderByDescending(x => x)), "Column not sorted in descending order");
        }

        public void CompareSortedValues(int row, string currentValue, string nextValue, string sortType)
        {
            int comparison = String.Compare(currentValue, nextValue);
            StringBuilder assertMsg = new($"Sorting not correct on position {row}.");
            assertMsg
                .AppendLine($"Value on postition {row}: {currentValue}.")
                .AppendLine($"Value on postition {row + 1}: {nextValue}");
            if (sortType == "asc")
            {
                Assert.IsTrue(comparison <= 0, assertMsg.ToString());
            }
            else if (sortType == "desc")
            {
                Assert.IsTrue(comparison >= 0, assertMsg.ToString());
            }
        }

        
    }
}