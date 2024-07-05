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
        if (IsLoggedIn()) // проверяем залогинены или нет
        {
            if (IsLoggedIn(account)) // проверяем залогинены ли мы под учетной записью которая передана в качестве параметра 
            {
                return; // если ок, то ничего не делаем login или logout
            }
            LogOut(); // если залогинены, но не под нужной записью - то loguot
        }
        // если проверили и выяснилось что не залогинены, то выполняется код для входа в систему
        Type(By.Name("user"), account.Username);
        Type(By.Name("pass"), account.Password);
        driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
    }
    
    public void LogOut()
    {
        if (IsLoggedIn()) 
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
    public bool IsLoggedIn() // проверка находимся ли мы внутри сессии, вошли в приложение
    {
        return IsElementPresent(By.Name("logout"));
    }
    public bool IsLoggedIn(AccountData account) // проверяем что мы залогинены под нужным пользователем, имя пользователя
    {
        return IsLoggedIn() // залогинены и под нужным пользовтелем
               && driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text
               == "(" + account.Username +")";
    }
}