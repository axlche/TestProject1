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

namespace TestProject1.PageObjects
{
    public class CreateUserPage : BasePage
    { 
        public IWebElement EmailInput => FindElement(By.Id("email"));
        public IWebElement PasswordInput => FindElement(By.Id("password"));
        public IWebElement Address1Input => FindElement(By.Id("address1"));
        public IWebElement Address2Input => FindElement(By.Id("address2"));
        public IWebElement CityInput => FindElement(By.Id("city"));
        public IWebElement ZipInput => FindElement(By.Id("zip"));
        public IWebElement AnnualInput => FindElement(By.Id("anual"));
        public IWebElement DescriptionInput => FindElement(By.Id("description"));
        public IWebElement BtnCreate => FindElement(By.CssSelector("button[type='submit']"));

        public CreateUserPage(IWebDriver driver) : base(driver) { }

        public void CreateNewUser(string email, string password, string address1, string address2, string city, string zip, string annualPayment, string description)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            Address1Input.SendKeys(address1);
            Address2Input.SendKeys(address2);
            CityInput.SendKeys(city);
            ZipInput.SendKeys(zip);
            AnnualInput.SendKeys(annualPayment);
            DescriptionInput.SendKeys(description);

            BtnCreate.Click();
        }

        public void CreateRandomUser()
        {
            var email = RandomString() + "@test.ts";
            var pswd = RandomString();
            var addr1 = RandomString();
            var addr2 = RandomString();
            var city = RandomString();
            var zip = "001266";
            var anual = "100";
            var descrp = RandomString();

            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(pswd);
            Address1Input.SendKeys(addr1);
            Address2Input.SendKeys(addr2);
            CityInput.SendKeys(city);
            ZipInput.SendKeys(zip);
            AnnualInput.SendKeys(anual);
            DescriptionInput.SendKeys(descrp);
            //EmailInput.SendKeys(RandomString() + "@test.ts");
            //PasswordInput.SendKeys(RandomString());
            //Address1Input.SendKeys(RandomString());
            //Address2Input.SendKeys(RandomString());
            //CityInput.SendKeys(RandomString());
            //ZipInput.SendKeys("001266");
            //AnnualInput.SendKeys("100");
            //DescriptionInput.SendKeys(RandomString());

            BtnCreate.Click();

            ListOfUsersPage.Ver
        }

        public void CreateWithUserObj(User user)
        {
            EmailInput.SendKeys(user.Email);
            PasswordInput.SendKeys(user.Password);
            Address1Input.SendKeys(user.Address1);
            Address2Input.SendKeys(user.Address2);
            CityInput.SendKeys(user.City);
            ZipInput.SendKeys(user.Zip);
            AnnualInput.SendKeys(user.AnnualPayment);
            DescriptionInput.SendKeys(user.Description);

            BtnCreate.Click();
        }

        public String RandomString()
        {
            Random random = new Random();
            int length = 10;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int num = random.Next(0, 26);

                char c = Convert.ToChar(num + 'a');
                sb.Append(c);
            }
            string randomString = sb.ToString();

            return randomString;
        }

        
    }
}
