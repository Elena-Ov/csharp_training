using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {

        private string baseUrl;

        private static AccountData admin = new AccountData
        {
            UserName = "administrator",
            Password = "password",
            Email = "root@localhost"
        };
        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }
        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table tbody tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                //для вырезания идентификатора используем регулярное выражение
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;
                accounts.Add(new AccountData() 
                    { UserName = name, Id = id });
            }
            return accounts;
        }
        public void DeleteAccount (AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("form[id='manage-user-delete-form'] input[type='submit']")).Click();
            //driver.FindElement(By.CssSelector("form[id='manage-user-delete-form']")).Click();
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }
        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            return driver;
        }

    }
}

