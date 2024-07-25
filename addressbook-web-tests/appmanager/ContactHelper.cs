using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests;

public class ContactHelper : HelperBase
{
    protected string baseURL = "http://localhost";

    public ContactHelper(ApplicationManager manager) : base(manager)
    {
    }

    public ContactHelper CreateContact(ContactFormData personalData)
    {
        manager.Navigator.OpenHomePage();
        AddNewPage();
        NewContactFillinForm(personalData);
        SubmitContactForm();
        ReturnToHomePage();
        return this;
    }

    public ContactHelper ModifyContacts(int p, ContactFormData modifiedPersonalData)
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

    public ContactHelper NewContactFillinForm(ContactFormData personalData)
    {
        Type(By.Name("firstname"), personalData.Firstname);
        Type(By.Name("lastname"), personalData.Lastname);
        //driver.FindElement(By.Name("firstname")).Click();
        // driver.FindElement(By.Name("lastname")).Click();
        return this;
    }

    public ContactHelper SubmitContactForm()
    {
        driver.FindElement(By.Name("submit")).Click();
        contactCache = null; // очистка кеша
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
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr[" + (index + 1) + "]/td[1]/input"))
            .Click();
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
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr[" + (index + 1) + "]/td[7]/a/img"))
            .Click();
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

    // сохраняем заполленный список контактов
    // вначала он пустой
    private List<ContactFormData> contactCache = null;

    public List<ContactFormData> GetContactsList()
    {
        //при первом обращении, когда выполняется GetContactsList() мы список заполняем
        //при втором используем ранее заполненный
        if (contactCache == null) //заполняем
        {
            contactCache = new List<ContactFormData>();
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
                    contactCache.Add(new ContactFormData(lastname, firstname)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
        }

        return new List<ContactFormData>(contactCache); // возвращаем копию
    }

    public int GetContactCount()
    {
        /*
         int index = 0;
         return driver.FindElements(By.TagName("tr[" + (index+1) + "]")).Count;*/
        // размер списка -1
        return driver.FindElements(By.TagName("tr")).Count - 1;
    }

    public ContactFormData GetContactInformationFromTable(int index)
    {
        manager.Navigator.OpenHomePage();
        //берем ячейки, сохраняем в переменную
        IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
            .FindElements(By.TagName("td"));
        //извлекаем из ячеек текст который нас интересует
        string lastName = cells[1].Text;
        string firstName = cells[2].Text;
        string address = cells[3].Text;
        // сохраняем номера телефонов целиком, потом делаем property
        string allPhones = cells[5].Text;
        string allEmails = cells[4].Text;
        return new ContactFormData(firstName, lastName)
        {
            Address = address,
            AllPhones = allPhones,
            AllEmails = allEmails
        };
    }
    public ContactFormData GetContactInformationFromEditForm(int index)
    {
        manager.Navigator.OpenHomePage();
        InitContactModification(0);
        string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
        string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
        string address = driver.FindElement(By.Name("address")).GetAttribute("value");
        string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
        string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
        string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
        // проверить
        string email = driver.FindElement(By.Name("email")).GetAttribute("value");
        string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
        string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
        // полученные данные заносим в объект типа ContactFormData
        return new ContactFormData(firstName, lastName)
        {
            Address = address,
            HomePhone = homePhone,
            MobilePhone = mobilePhone,
            WorkPhone = workPhone,
            Email = email,
            Email2 = email2,
            Email3 = email3
        };
    }

public void InitContactModification(int index)
    {
       driver.FindElements(By.Name("entry"))[index] // взяли все строки, потом по индексу
           // внутри строки взяли все ячейки, нашли ту в которой кнопка редактирования
           .FindElements(By.TagName("td"))[7]
           // внутри нее находим ссылку
           .FindElement(By.TagName("a")).Click();
    }

    public int GetNumberOfSearchResults()
    {
        manager.Navigator.OpenHomePage();
        string text = driver.FindElement(By.TagName("label")).Text;
        // создаем регулярное выражение, применяем его к строке и забираем ту часть строки, которая удовлетворяет этому рег выражению
        // в качестве результата получаем объект специального типа Match
        // выполняем некоторые проверки с этим объектом
        Match m = new Regex(@"\d+").Match(text);
        return Int32.Parse(m.Value);
    }
}