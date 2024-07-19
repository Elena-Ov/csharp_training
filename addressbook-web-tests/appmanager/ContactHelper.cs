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

public class ContactHelper : HelperBase
{    
    protected string baseURL = "http://localhost";
    public ContactHelper(ApplicationManager manager): base(manager)
    {
    }

    public ContactHelper CreateContact(ContactForm personalData)
    { 
        manager.Navigator.OpenHomePage();
        AddNewPage(); 
        NewContactFillinForm(personalData); 
        SubmitContactForm(); 
        ReturnToHomePage();
        return this;
    }

    public ContactHelper ModifyContacts(int p, ContactForm modifiedPersonalData)
    {
        manager.Navigator.OpenHomePage();
        SelectContact(p);
        SelectDetails(p);
        ModifyData();
        NewContactFillinForm(modifiedPersonalData);
        SubmitContactModification();
        ReturnToHomePage();
        return this;
    }
    
    public ContactHelper RemovePersonalData(int p)
    { 
        manager.Navigator.OpenHomePage();
        SelectContact(p);
        RemoveContact();
        return this;
    }
    
    public ContactHelper AddNewPage()
    {
        driver.FindElement(By.LinkText("add new")).Click();
        driver.Navigate().GoToUrl("http://localhost/addressbook/edit.php");
        return this;
    }
    public ContactHelper NewContactFillinForm(ContactForm personalData)
    {
        Type(By.Name("firstname"),personalData.Firstname);
        Type(By.Name("lastname"),personalData.Lastname);
        //driver.FindElement(By.Name("firstname")).Click();
       // driver.FindElement(By.Name("lastname")).Click();
        return this;
    }
    public ContactHelper SubmitContactForm()
    {
        driver.FindElement(By.Name("submit")).Click();
        contactCache = null;
        return this;
    }
    public ContactHelper ReturnToHomePage()
    {
        driver.FindElement(By.LinkText("home page")).Click();
        driver.Navigate().GoToUrl("http://localhost/addressbook/index.php");
        return this;
    }

    public ContactHelper SelectContact(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr["+ (index+1) +"]/td[1]/input")).Click();
        return this;
    }

    public ContactHelper RemoveContact()
    {
        driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
        contactCache = null;
        return this;
    }

    public ContactHelper SelectDetails(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr["+ (index+1) +"]/td[7]/a/img")).Click();
        return this;
    }

    public ContactHelper ModifyData()
    {
        driver.FindElement(By.Name("modifiy")).Click();
        return this;
    }

    public ContactHelper SubmitContactModification()
    {
        driver.FindElement(By.Name("update")).Click();
        contactCache = null;
        return this;
    }
    public bool IsContactFound() // проверка есть ли хотя бы один контакт int index, ContactForm personalData
    {
        return driver.Url == baseURL + "/addressbook/index.php" &&
               IsElementPresent(By.Name("selected[]"));
    }

    private List<ContactForm> contactCache = null;

    public List<ContactForm> GetContactsList()
    {
        if (contactCache == null)
        {
            contactCache = new List<ContactForm>();
            manager.Navigator.OpenHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.TagName("tr"));
            string firstname;
            string lastname;
            foreach (IWebElement element in elements)
            {
                if (element.GetAttribute("class") == null)
                    continue;
                var divs = element.FindElements(By.TagName("td"));
                if (divs.Count() > 0)
                {
                    lastname = divs[1].Text;
                    firstname = divs[2].Text;
                    contactCache.Add(new ContactForm(lastname, firstname)
                    {
                        Id = element.FindElement(By.TagName("td")).GetAttribute("id")
                    });
                }
            }
        }
        return new List<ContactForm>(contactCache);
    }
    public int GetContactCount()
    {
       // чтобы считал со 2-го tr
        //int index = 0;
        //return driver.FindElements(By.TagName("tr[" + (index+1) + "]")).Count;
        // -1 так как у нас первый tr заголовок
        return driver.FindElements(By.TagName("tr")).Count -1;
    }
}