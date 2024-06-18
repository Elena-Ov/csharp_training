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
   [TestFixture]
   public class AddNewContact
   {
       private IWebDriver driver;
       private StringBuilder verificationErrors;
       private string baseURL;
       private bool acceptNextAlert = true;
      
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
      
       [Test]
       public void TheAddNewContactTest()
       {   OpenHomePage();
           Login(new AccountData("admin", "secret"));
           AddNewPage();
           NewContactFillinForm(new ContactForm("Sam", "Smirnof"));
           SubmitContactForm();
           ReturnToHomePage();
       }

       private void Login(AccountData account)
       {
           driver.FindElement(By.Name("user")).Click();
           driver.FindElement(By.Name("user")).Clear();
           driver.FindElement(By.Name("user")).SendKeys(account.Username);
           driver.FindElement(By.Name("pass")).Click();
           driver.FindElement(By.Name("pass")).Clear();
           driver.FindElement(By.Name("pass")).SendKeys(account.Password);
           driver.FindElement(By.XPath("//input[@value='Login']")).Click();
       }

       private void OpenHomePage()
       {
           driver.Navigate().GoToUrl(baseURL);
       }

       private void ReturnToHomePage()
       {
           driver.FindElement(By.LinkText("home page")).Click();
           driver.Navigate().GoToUrl("http://localhost/addressbook/index.php");
           driver.FindElement(By.LinkText("Logout")).Click();
       }

       private void SubmitContactForm()
       {
           driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
       }

       private void NewContactFillinForm(ContactForm personalData)
       {
           driver.FindElement(By.Name("firstname")).Click();
           driver.FindElement(By.Name("firstname")).Clear();
           driver.FindElement(By.Name("firstname")).SendKeys(personalData.Firstname);
           driver.FindElement(By.Name("lastname")).Click();
           driver.FindElement(By.Name("lastname")).Clear();
           driver.FindElement(By.Name("lastname")).SendKeys(personalData.Lastname);
       }

       private void AddNewPage()
       {
           driver.FindElement(By.LinkText("add new")).Click();
           driver.Navigate().GoToUrl("http://localhost/addressbook/edit.php");
       }

       private bool IsElementPresent(By by)
       {
           try
           {
               driver.FindElement(by);
               return true;
           }
           catch (NoSuchElementException)
           {
               return false;
           }
       }
      
       private bool IsAlertPresent()
       {
           try
           {
               driver.SwitchTo().Alert();
               return true;
           }
           catch (NoAlertPresentException)
           {
               return false;
           }
       }
      
       private string CloseAlertAndGetItsText() {
           try {
               IAlert alert = driver.SwitchTo().Alert();
               string alertText = alert.Text;
               if (acceptNextAlert) {
                   alert.Accept();
               } else {
                   alert.Dismiss();
               }
               return alertText;
           } finally {
               acceptNextAlert = true;
           }
       }
   }
}