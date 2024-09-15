using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using mantis_tests.appmanager;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MantisTests;

public class ApplicationManager
{   
    protected IWebDriver driver;
    public string baseURL;
    ////ссылки на помощники
    protected LoginHelper loginHelper;
    protected NavigationHelper navigator;
    protected ProjectManagementHelper projectHelper;
    
    private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>(); //единственный экземпляр ApplicationManager
    
    //конструктор ApplicationManager
    private ApplicationManager() 
    {   
        driver = new ChromeDriver();
        baseURL = "http://localhost/mantisbt-2.26.3/";
        //передаем ссылку на ApplicationManager
        loginHelper = new LoginHelper(this);
        navigator = new NavigationHelper(this);
        projectHelper = new ProjectManagementHelper(this);
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        //registrationHelper = new RegistrationHelper(this);
        //Registration = new RegistrationHelper(this);
        //Ftp = new FtpHelper(this);
        //James = new JamesHelper(this);
        //Mail = new MailHelper(this);

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
    public LoginHelper Auth { get { return loginHelper; } }

    public NavigationHelper Navigator { get { return navigator; } }
    
    public ProjectManagementHelper ProjectHelper { get { return projectHelper; } }
    
    public IWebDriver Driver { get { return driver; } }

    public string BaseUrl { get { return baseURL; } }
    
    //автопроперти
    public RegistrationHelper Registration { get ; set; }
    
    public FtpHelper Ftp { get; set; }
    public JamesHelper James { get; set; }
    public MailHelper Mail { get; set; }
    public ProjectManagementHelper Projects
    {
        get { return projectHelper; }
    }
    
}