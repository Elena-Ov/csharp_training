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

public class LoginHelper : HelperBase
{
    public LoginHelper(IWebDriver driver) : base(driver) // вызов конструктора базового класса
    {
    }
    public void Login(AccountData account) // параметр типа AccountData
    {
        driver.FindElement(By.Name("user")).Click();
        driver.FindElement(By.Name("user")).Clear();
        driver.FindElement(By.Name("user")).SendKeys(account.Username); // данные которые передаются в этом параметре
        driver.FindElement(By.Name("pass")).Click();
        driver.FindElement(By.Name("pass")).Clear();
        driver.FindElement(By.Name("pass")).SendKeys(account.Password);
        driver.FindElement(By.XPath("//input[@value='Login']")).Click();
    }
    public void LogOut()
    {
        driver.FindElement(By.LinkText("Logout")).Click();
    }
}