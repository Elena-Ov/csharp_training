using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MantisTests;

public class ApplicationManager
{   
    protected IWebDriver driver;
    public string baseURL;
    
    private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>(); //единственный экземпляр ApplicationManager
    
    //конструктор ApplicationManager
    private ApplicationManager() 
    {   
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        baseURL = "http://localhost/addressbook/";
        //registrationHelper = new RegistrationHelper(this);
        Registration = new RegistrationHelper(this);
        Ftp = new FtpHelper(this);
        James = new JamesHelper(this);
        Mail = new MailHelper(this);

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
            newInstance.driver.Url = "http://localhost/mantisbt-2.26.3/login_page.php";
            app.Value = newInstance;
        }
        return app.Value;
    }
    public IWebDriver Driver { get { return driver; } }

    public string BaseUrl { get { return baseURL; } }
    
    //автопроперти
    public RegistrationHelper Registration { get ; set; }
    
    public FtpHelper Ftp { get; set; }
    public JamesHelper James { get; set; }
    public MailHelper Mail { get; set; }
    
}