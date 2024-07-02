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
    public LoginHelper(ApplicationManager manager) 
        : base(manager) // вызов конструктора базового класса
    {
    }
    public void Login(AccountData account) // параметр типа AccountData
    {
        Type(By.Name("user"), account.Username);
        Type(By.Name("pass"), account.Password);
        //driver.FindElement(By.Name("user")).Click();
        //driver.FindElement(By.Name("pass")).Click();
        driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
    }
    public void LogOut()
    {
        driver.FindElement(By.LinkText("Logout")).Click();
    }
}