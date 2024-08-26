using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests 
{
    public class NavigationHelper : HelperBase
{   
    public string baseURL;
    public NavigationHelper(ApplicationManager manager) 
        : base(manager)
    { 
        baseURL = manager.baseURL;//переданное значение объект будет присваиваться в поле baseURL
    }
    
    public NavigationHelper OpenHomePage()
    {
        if (driver.Url == baseURL) // + "http://localhost/addressbook" && IsElementPresent(By.Name("new"))) 
        {
            return this;
        }

        driver.Navigate().GoToUrl(baseURL);  // + "http://localhost/addressbook");
        return this;
    }
    
    public NavigationHelper GoToGroupsPage()
    {
        if (driver.Url == baseURL + "group.php" && IsElementPresent(By.Name("new"))) 
        {
            return this;
        }
        driver.FindElement(By.LinkText("groups")).Click();
        return this;
    }

    // для ContactInformationTests
    public NavigationHelper GoToContactsPage()
    {
        if (driver.Url == baseURL)
        {
            return this;
        }
        driver.FindElement(By.LinkText("home")).Click();
        manager.Contact.ClearGroupFilter();
        return this;
    }
    // для ContactInformationTests

    public NavigationHelper GoToContactInformationFromIdPage(int index)
    {
        manager.Navigator.GoToContactsPage();
        driver.FindElements(By.Name("entry"))[index]
            .FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
        return this;
    }
}
    
}

