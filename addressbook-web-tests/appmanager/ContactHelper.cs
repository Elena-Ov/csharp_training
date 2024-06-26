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
        driver.FindElement(By.Name("firstname")).Click();
        driver.FindElement(By.Name("firstname")).Clear();
        driver.FindElement(By.Name("firstname")).SendKeys(personalData.Firstname);
        driver.FindElement(By.Name("lastname")).Click();
        driver.FindElement(By.Name("lastname")).Clear();
        driver.FindElement(By.Name("lastname")).SendKeys(personalData.Lastname);
        return this;
    }
    public ContactHelper SubmitContactForm()
    {
        driver.FindElement(By.Name("submit")).Click();
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
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr["+ index +"]/td[1]/input")).Click();
        return this;
    }

    public ContactHelper RemoveContact()
    {
        driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
        return this;
    }

    public ContactHelper SelectDetails(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr["+ index +"]/td[7]/a/img")).Click();
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
        return this;
    }
}