using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace MantisTests
{
    public class AdminHelper : HelperBase
    {

        private string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }
        /*public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();

            IWebDriver driver = OpenAppAndLoging();
            driver.Url = baseUrl + "manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table tbody tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;
                //accounts.Add(new AccountData() { Name = username, Id = id });
            }
            return accounts;
        }
        public void DeleteAccount (AccountData account)
        {
            IWebDriver driver = OpenAppAndLoging();
            driver.Url = baseUrl + "manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("form[id='manage-user-delete-form'] input[type='submit']")).Click();
            //driver.FindElement(By.CssSelector("form[id='manage-user-delete-form']")).Click();
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }
        private IWebDriver OpenAppAndLoging()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            return driver;
        }*/

    }
}

