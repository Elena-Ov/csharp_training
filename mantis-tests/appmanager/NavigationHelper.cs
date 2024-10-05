using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_tests

{
    public class NavigationHelper : HelperBase
    {
        public string baseURL;
        public NavigationHelper(ApplicationManager manager) : base(manager)
        { 
            baseURL = manager.baseURL;
        }
        //переход с помощью Navigate()
        public NavigationHelper OpenLoginPage()
        {
            if (driver.Url == baseURL + "login_page.php")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "login_page.php");
            return this;
        }
        public NavigationHelper OpenHomePage()
        {
            if (driver.Url == baseURL + "my_view_page.php")
            {
                return this;
            }
            driver.Navigate().GoToUrl(baseURL + "my_view_page.php");
            return this;
        }

        
        public NavigationHelper GoToManagementPage()
        {
            if (driver.Url == baseURL + "manage_overview_page.php") 
            {
                return this;
            }
            //driver.FindElement(By.XPath("//*[@id=\"sidebar\"]/ul/li[7]/a/span")).Click();
            driver.Navigate().GoToUrl(baseURL + "manage_overview_page.php");
            return this;
        }

        public NavigationHelper GoToProjectManagementPage()
        {
            if (driver.Url == baseURL + "manage_proj_page.php") 
            {
                return this;
            }
            //driver.FindElement(By.XPath("//*[@id=\"main-container\"]/div[2]/div[2]/div/ul/li[3]/a")).Click();
            driver.Navigate().GoToUrl(baseURL + "manage_proj_page.php");
            return this;
        }

        public NavigationHelper GoToProjectCreationPage()
        {
            if (driver.Url == baseURL + "manage_proj_create_page.php") 
            {
                return this;
            }
            //driver.FindElements(By.XPath("//button[@type='submit']"))[0].Click();
            driver.Navigate().GoToUrl(baseURL + "manage_proj_create_page.php");
            return this;
        }
    } 
}
