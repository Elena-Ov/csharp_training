using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests;

public class NavigationHelper : HelperBase
{   
    protected string baseURL;
    public NavigationHelper(ApplicationManager manager, string baseUrl) 
        : base(manager)
    { 
        this.baseURL = baseURL;//переданное значение объект будет присваиваться в поле baseURL
    }
    
    public void OpenHomePage()
    {
        if (driver.Url == baseURL + "http://localhost/addressbook" && IsElementPresent(By.Name("new"))) 
        {
            return;
        }
        driver.Navigate().GoToUrl(baseURL + "http://localhost/addressbook");
    }
    
    public void GoToGroupsPage()
    {
        if (driver.Url == baseURL + "http://localhost/addressbook/group.php" && IsElementPresent(By.Name("new"))) 
        {
            return;
        }
        driver.FindElement(By.LinkText("groups")).Click();
    }
}