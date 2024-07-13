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
    private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>(); //единственный экземпляр ApplicationManager
    
    private ApplicationManager() 
    {   
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        baseURL = "http://localhost"; //"/addressbook";
        
        loginHelper = new LoginHelper(this); //передаем ссылку на ApplicationManager
        navigator = new NavigationHelper(this, baseURL);
        groupHelper = new GroupHelper(this);
        contactHelper = new ContactHelper(this);
    }

     ~ApplicationManager()// деструктор, вызывается автоматически, модификатор доступа не нужен
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
    public static ApplicationManager GetInstance() // глобальный метод, который может быть вызван не в конкретном объекте, а по его имени
    {
        if (! app.IsValueCreated)
        {
            ApplicationManager newInstance = new ApplicationManager();
            newInstance.Navigator.OpenHomePage();
            app.Value = newInstance;
        }
        return app.Value;
    }
    public IWebDriver Driver 
    {
        get
        {
            return driver;
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