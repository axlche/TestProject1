using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject1.Base;
using TestProject1.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Faker;
using System.Configuration;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Tests : BaseClass
    {

        public Dictionary<string, string> userData;

        public Tests()
        {
            userData = new Dictionary<string, string>();
            userData["email"] = "dictionary@email.com";
            userData["password"] = "password";
            userData["address1"] = "123 Random St.";
            userData["address2"] = "Apt. 12";
            userData["city"] = "RandomCity";
            userData["zip"] = "100001";
            userData["annualPayment"] = "225";
            userData["description"] = "Random description provided";
        }

        [Test]
        public void Homework1()
        {
            LoginPage loginPage = new(driver);
            CoursePage coursePage = new(driver);

            loginPage.Login(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);

            coursePage.WaitForSpinner();

            coursePage.VerifyLoggedIn("John Walker");

            coursePage.WaitFirstNavMenuLoaded();
            coursePage.VerifyColorNotRed(coursePage.DashboardMenuItem);
            coursePage.VerifyColorNotRed(coursePage.OrdersMenuItem);
            coursePage.VerifyColorNotRed(coursePage.ProductsMenuItem);
            coursePage.VerifyColorNotRed(coursePage.CustomersMenuItem);
            coursePage.VerifyColorNotRed(coursePage.ReportsMenuItem);
            coursePage.VerifyColorNotRed(coursePage.IntegrationsMenuItem);
            coursePage.VerifyColorNotRed(coursePage.CrtUsersMenuItem);
            coursePage.VerifyColorNotRed(coursePage.CrtManagerMenuItem);
            coursePage.VerifyColorNotRed(coursePage.CrtSubsMenuItem);
            coursePage.VerifyColorNotRed(coursePage.LstOfUsersMenuItem);
            coursePage.VerifyColorNotRed(coursePage.LstOfSubsMenuItem);
        }

        [Test]
        public void Homework2()
        {
            LoginPage loginPage = new(driver);
            CoursePage coursePage = new(driver);
            loginPage.Login(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);

            coursePage.WaitForSpinner();

            driver.FindElement(By.TagName("body")).SendKeys(Keys.End);
            Thread.Sleep(200);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
            wait.Until(ExpectedConditions.TextToBePresentInElement(coursePage.BtnCheckStatus, "Check Status"));

            coursePage.BtnCheckStatus.Click();

            wait.Until(ExpectedConditions.TextToBePresentInElement(coursePage.BtnCheckStatus, "Loading.."));
            wait.Until(ExpectedConditions.TextToBePresentInElement(coursePage.BtnCheckStatus, "Active"));

            Assert.IsTrue(coursePage.BtnCheckStatus.Text == "Active");

            coursePage.IdHeader.Click();
            coursePage.TableSorting("id", "asc");
            coursePage.IdHeader.Click();
            coursePage.TableSorting("id", "desc");
            coursePage.NameHeader.Click();
            coursePage.TableSorting("name", "asc");
            coursePage.NameHeader.Click();
            coursePage.TableSorting("name", "desc");
            coursePage.AgeHeader.Click();
            coursePage.TableSorting("age", "asc");
            coursePage.AgeHeader.Click();
            coursePage.TableSorting("age", "desc");

        }

        [Test]
        public void Homework3CreatingUserFromJson()
        {
            LoginPage loginPage = new(driver);
            CoursePage coursePage = new(driver);
            CreateUserPage createUserPage = new(driver);
            ListOfUsersPage listOfUsersPage = new(driver);
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDirectory, "json1.json");
            string jsonString = File.ReadAllText(filePath);
            User user = JsonConvert.DeserializeObject<User>(jsonString);

            loginPage.Login(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);
            coursePage.WaitForSpinner();// Verifying number of rows existed in table before 1st user creation
            coursePage.LstOfUsersMenuItem.Click();
            var rowsNumber = listOfUsersPage.Rows.Count;

            // Creating from JSON file
            
            coursePage.CrtUsersMenuItem.Click();
            createUserPage.EmailInput.SendKeys(user.Email);
            createUserPage.PasswordInput.SendKeys(user.Password);
            createUserPage.Address1Input.SendKeys(user.Address1);
            createUserPage.Address2Input.SendKeys(user.Address2);
            createUserPage.CityInput.SendKeys(user.City);
            createUserPage.ZipInput.SendKeys(user.Zip);
            createUserPage.AnnualInput.SendKeys(user.AnnualPayment);
            createUserPage.DescriptionInput.SendKeys(user.Description);
            createUserPage.BtnCreate.Click();
            Thread.Sleep(2000);
            
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber));
        }

        [Test]
        public void Homework3CreatingUserFromDictionary()
        {
            var user = new User()
            {
                Email = "email@email.com",
                Password = "password1234",
                Address1 = "123 Street",
                Address2 = "45",
                City = "City",
                Zip = "0001",
                AnnualPayment = "200",
                Description = "description description new line"
            };
            LoginPage loginPage = new(driver);
            CoursePage coursePage = new(driver);
            CreateUserPage createUserPage = new(driver);
            ListOfUsersPage listOfUsersPage = new(driver);

            loginPage.Login(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);

            coursePage.WaitForSpinner();


            // Verifying number of rows existed in table before 1st user creation
            coursePage.LstOfUsersMenuItem.Click();
            var rowsNumber = listOfUsersPage.Rows.Count;

            // Creating user using dictionary
            coursePage.CrtUsersMenuItem.Click();
            createUserPage.CreateWithUserObj(user);
            //createUserPage.CreateNewUser(user.Email, user.Password, user)
            //createUserPage.CreateNewUser(userData["email"], userData["password"], userData["address1"], userData["address2"], userData["city"], userData["zip"], userData["annualPayment"], userData["description"]);
            Thread.Sleep(2000);
            listOfUsersPage.VerifyCreatedUser(rowsNumber, user.Email, user.Address1, user.Address2, user.City, user.Zip, user.Description, user.AnnualPayment);


        }

        [Test]
        public void Homework3CreatingUserWithFaker()
        {
            LoginPage loginPage = new(driver);
            loginPage.Login(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);

            CoursePage coursePage = new(driver);
            coursePage.WaitForSpinner();

            CreateUserPage createUserPage = new(driver);

            // Verifying number of rows existed in table before 1st user creation
            coursePage.LstOfUsersMenuItem.Click();
            ListOfUsersPage listOfUsersPage = new(driver);
            var rowsNumber = listOfUsersPage.Rows.Count;

            // Creating using Faker
            coursePage.CrtUsersMenuItem.Click();
            createUserPage.EmailInput.SendKeys(Internet.Email());
            createUserPage.PasswordInput.SendKeys(Lorem.GetFirstWord());
            createUserPage.Address1Input.SendKeys(Address.StreetAddress());
            createUserPage.Address2Input.SendKeys(Address.SecondaryAddress());
            createUserPage.CityInput.SendKeys(Address.City());
            createUserPage.ZipInput.SendKeys(Address.ZipCode());
            createUserPage.AnnualInput.SendKeys(RandomNumber.Next(1, 100).ToString());
            createUserPage.DescriptionInput.SendKeys(Lorem.Sentence());
            createUserPage.BtnCreate.Click();
            Thread.Sleep(2000);
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber));
        }

        [Test]
        public void Homework3CreatingFirstUser()
        {
            LoginPage loginPage = new(driver);
            loginPage.Login(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);

            CoursePage coursePage = new(driver);
            coursePage.WaitForSpinner();

            CreateUserPage createUserPage = new(driver);

            // Verifying number of rows existed in table before 1st user creation
            coursePage.LstOfUsersMenuItem.Click();
            ListOfUsersPage listOfUsersPage = new(driver);
            var rowsNumber = listOfUsersPage.Rows.Count;

            //Create first user
            coursePage.CrtUsersMenuItem.Click();
            createUserPage.CreateRandomUser(); ;
            Thread.Sleep(2000);
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber));
        }


        //[Test]
        public void Draft()
        {


            // Creating from XLSX file
            //Application excel = new Application();
            //Workbook workbook = excel.Workbooks.Open("userXlsx.xlsx");
            //Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            //createUserPage.EmailInput().SendKeys((string)worksheet.Cells[2, 1].Value2);
            //createUserPage.PasswordInput().SendKeys(worksheet.Cells[2, 2].GetValue<string>());
            //createUserPage.Address1Input().SendKeys(worksheet.Cells[2, 3].GetValue<string>());
            //createUserPage.Address2Input().SendKeys(user.Address2);
            //createUserPage.CityInput().SendKeys(user.City);
            //createUserPage.ZipInput().SendKeys(user.Zip);
            //createUserPage.AnnualInput().SendKeys(user.AnnualPayment);
            //createUserPage.DescriptionlUser().SendKeys(user.Description);
            //createUserPage.BtnCreate().Click();
        }

        
    }
}