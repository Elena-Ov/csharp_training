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

public class ApplicationManager
{   
    protected IWebDriver driver;
    protected string baseURL;
    //ссылки на помощники
    protected LoginHelper loginHelper;
    protected NavigationHelper navigator;
    protected GroupHelper groupHelper;
    protected ContactHelper contactHelper;
    
    public ApplicationManager()
    {   
        driver = new ChromeDriver(); 
        baseURL = "http://localhost/addressbook"; // второй слеш не нужен, пока работает с ним
        
        loginHelper = new LoginHelper(this); //передаем ссылку на ApplicationManager
        navigator = new NavigationHelper(this, baseURL);
        groupHelper = new GroupHelper(this);
        contactHelper = new ContactHelper(this);
    }
    public IWebDriver Driver 
    {
        get
        {
            return driver;
        } 
    }
    public void Stop() // метод для остановки внутри ApplicationManager, код для остановки браузера
    {
        try
        {
            driver.Quit();
        }
        catch (Exception)
        {
            // Ignore errors if unable to close the browser
        }
    } 
    // поля не меняем на public, а делаем для них property только с геттером
    public LoginHelper Auth
    {
        get
        {
            return loginHelper; // будет возвращать поле с таким именем
        }
    }

    public NavigationHelper Navigator
    {
        get
        {
            return navigator;
        }
    }

    public GroupHelper Groups
    {
        get
        {
            return groupHelper;
        }
    }

    public ContactHelper Contact
    {
        get
        {
            return contactHelper;
        }
    }
}