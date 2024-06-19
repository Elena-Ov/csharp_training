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

public class TestBase
{
    protected IWebDriver driver;
    private StringBuilder verificationErrors;
    protected string baseURL;
    
    [SetUp]
    public void SetupTest()
    {
        driver = new ChromeDriver();
        baseURL = "http://localhost/addressbook/";
        verificationErrors = new StringBuilder();
    }
        
    [TearDown]
    public void TeardownTest()
    {
        try
        {
            driver.Quit();
        }
        catch (Exception)
        {
            // Ignore errors if unable to close the browser
        }
        Assert.AreEqual("", verificationErrors.ToString());
    }
    protected void OpenHomePage()
    {
        driver.Navigate().GoToUrl(baseURL);
    }
    protected void Login(AccountData account) // параметр типа AccountData
    {
        driver.FindElement(By.Name("user")).Click();
        driver.FindElement(By.Name("user")).Clear();
        driver.FindElement(By.Name("user")).SendKeys(account.Username); // данные которые передаются в этом параметре
        driver.FindElement(By.Name("pass")).Click();
        driver.FindElement(By.Name("pass")).Clear();
        driver.FindElement(By.Name("pass")).SendKeys(account.Password);
        driver.FindElement(By.XPath("//input[@value='Login']")).Click();
    }
    protected void GoToGroupsPage()
    {
        driver.FindElement(By.LinkText("groups")).Click();
    }
    protected void InitGroupCreation()
    {
        driver.FindElement(By.Name("new")).Click();
    }
    protected void FillGroupForm(GroupData group)
    {
        driver.FindElement(By.Name("group_name")).Click();
        driver.FindElement(By.Name("group_name")).Clear();
        driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        driver.FindElement(By.Name("group_header")).Click();
        driver.FindElement(By.Name("group_header")).Clear();
        driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
        driver.FindElement(By.Name("group_footer")).Click();
        driver.FindElement(By.Name("group_footer")).Clear();
        driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
    }
    protected void SubmitGroupCreation()
    {
        driver.FindElement(By.Name("submit")).Click();
    }
    protected void ReturnToGroupsPage()
    {
        driver.FindElement(By.LinkText("group page")).Click();
    }
    protected void LogOut()
    {
        driver.FindElement(By.LinkText("Logout")).Click();
    }
    protected void SelectGroup(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form/span["+ index +"]/input")).Click();
    }
    protected void RemoveGroup()
    {
        driver.FindElement(By.Name("delete")).Click();
    }
    protected void AddNewPage()
    {
        driver.FindElement(By.LinkText("add new")).Click();
        driver.Navigate().GoToUrl("http://localhost/addressbook/edit.php");
    }
    protected void NewContactFillinForm(ContactForm personalData)
    {
        driver.FindElement(By.Name("firstname")).Click();
        driver.FindElement(By.Name("firstname")).Clear();
        driver.FindElement(By.Name("firstname")).SendKeys(personalData.Firstname);
        driver.FindElement(By.Name("lastname")).Click();
        driver.FindElement(By.Name("lastname")).Clear();
        driver.FindElement(By.Name("lastname")).SendKeys(personalData.Lastname);
    }
    protected void SubmitContactForm()
    {
        driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
    }
    protected void ReturnToHomePage()
    {
        driver.FindElement(By.LinkText("home page")).Click();
        driver.Navigate().GoToUrl("http://localhost/addressbook/index.php");
    }
}
