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
    private string baseURL;
    //создаем конструктор
    public NavigationHelper(IWebDriver driver, string baseURL) : base(driver)
    { 
        this.baseURL = baseURL;//переданное значение объект будет присваиваться в поле baseURL
    }
    
    public void OpenHomePage()
    {
        driver.Navigate().GoToUrl(baseURL);
    }
    
    public void GoToGroupsPage()
    {
        driver.FindElement(By.LinkText("groups")).Click();
    }
}