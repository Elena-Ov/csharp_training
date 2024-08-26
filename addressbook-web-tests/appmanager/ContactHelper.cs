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

    //модификация по индексу
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
    //модификация по уникальному идентификатору
    public ContactHelper ModifyContacts(ContactFormData contact, ContactFormData modifiedPersonalData)
    {
        manager.Navigator.OpenHomePage();
        SelectContact(contact.Id);
        SelectDetails(contact.Id);
        ModifyData();
        NewContactFillinForm(modifiedPersonalData);
        SubmitContactModification();
        ReturnToHomePage();
        return this;
    }

    // удаление по индексу
    public ContactHelper RemovePersonalData(int p)
    {
        manager.Navigator.OpenHomePage();
        SelectContact(p);
        RemoveContact();
        return this;
    }
    
    // удаление по уникальному идентификатору
    public ContactHelper RemovePersonalData(ContactFormData contact)
    {
        manager.Navigator.OpenHomePage();
        SelectContact(contact.Id);
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
    
    //выбираем по индексу
    public ContactHelper SelectContact(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form[2]/table/tbody/tr[" + (index + 1) + "]/td[1]/input"))
            .Click();
        //driver.FindElement(By.Id(contactId)).Click();
        return this;
    }
    
    //выбираем по идентификатору
    public ContactHelper SelectContact(string contactId)
    {
        driver.FindElement(By.XPath("//input[@name='selected[]' and @value='"+contactId+"']")).Click();
        //driver.FindElement(By.Id(contactId)).Click();
        return this;
    }

    public ContactHelper RemoveContact()
    {
        driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
        contactCache = null;
        return this;
    }

    // по индексу
    public ContactHelper SelectDetails(int index)
    {
        driver.FindElements(By.Name("entry"))[index] // взяли все строки, потом по индексу
            // внутри строки взяли все ячейки, нашли ту в которой кнопка редактирования
            .FindElements(By.TagName("td"))[6]
                // внутри нее находим ссылку
                .FindElement(By.TagName("a")).Click();
        return this;
    }
    
    // по идентификатору
    public ContactHelper SelectDetails(string id)
    {
        driver.FindElement(By.XPath("//a[contains(@href,'view.php?id="+id+"')]")).Click();
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

   /* public bool IsContactFound() // проверка есть ли хотя бы один контакт int index, ContactForm personalData
    {
        return driver.Url == baseURL + "/addressbook/index.php" &&
               IsElementPresent(By.Name("selected[]"));
    }*/
    public bool IsContactFound()
    {
        manager.Navigator.GoToContactsPage();
        return IsElementPresent(By.Name("entry"));
    }

    public bool IsContactFound(int index)
    {
        manager.Navigator.GoToContactsPage();
        ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
        return elements.Count > index;
    }

    public ContactHelper FindOrCreateContact(int index = 0)
    {
        while (!IsContactFound(index))
        {
            CreateContact(new ContactFormData("Man", "Rokki"));
        }

        return this;
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
    // для ContactInformationTests
    public ContactFormData GetContactInformationFromTable(int index)
    {
        manager.Navigator.GoToContactsPage();
        
        IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
            .FindElements(By.TagName("td"));
        
        return new ContactFormData()
        {
            Lastname = cells[1].Text,
            Firstname = cells[2].Text,
            Address = cells[3].Text,
            AllEmails = cells[4].Text,
            AllPhones = cells[5].Text
        };
    }
    // для ContactInformationTests
    public ContactFormData GetContactInformationFromEditForm(int index)
    {
        manager.Navigator.GoToContactsPage();
        InitContactModification(index); //карандаш
        return ReadContactFromEditForm();
    }
    // для ContactInformationTests

    public ContactFormData ReadContactFromEditForm()
    {
        return new ContactFormData()
        {
            Firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value"),
            Middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value"),
            Lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value"),
            Nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value"),
            Address = driver.FindElement(By.Name("address")).GetAttribute("value"),
            HomePhone = driver.FindElement(By.Name("home")).GetAttribute("value"),
            MobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value"),
            WorkPhone = driver.FindElement(By.Name("work")).GetAttribute("value"),
            Email = driver.FindElement(By.Name("email")).GetAttribute("value"),
            Email2 = driver.FindElement(By.Name("email2")).GetAttribute("value"),
            Email3 = driver.FindElement(By.Name("email3")).GetAttribute("value")
        };
    }
    // для ContactInformationTests
    
    public string GetContactInformationFromIdPage(int index)
    {
        // переход на страницу id
        manager.Navigator.GoToContactInformationFromIdPage(index);
        string infoFromIdPage = driver.FindElement(By.Id("content")).Text;
        
        return infoFromIdPage;
    }
    public void InitContactModification(int index) // карандаш
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

    public void AddContactToGroup(ContactFormData contact, GroupData group)
    {
        manager.Navigator.OpenHomePage();
        ClearGroupFilter();
        SelectContact(contact.Id);
        SelectGroupToAdd(group.Name);
        CommitAddingContactToGroup();
        // драйвер будет проверять загрузился ли элемент
        new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d=> d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
    }

    public void ClearGroupFilter()
    {
        new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
    }

    public void SelectGroupToAdd(string name)
    {
        new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
    }

    public void CommitAddingContactToGroup()
    {
        driver.FindElement(By.Name("add")).Click();
    }

    public void RemoveContactFromGroup(ContactFormData contact, GroupData group)
    {
        manager.Navigator.OpenHomePage();
        SelectGroupByFilter(group.Id);
        SelectContact(contact.Id);
        CommitRemovalFromGroup();
        new WebDriverWait (driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
    }

    public void SelectGroupByFilter(string id)
    {
        new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(id);
    }

    public void CommitRemovalFromGroup()
    {
        driver.FindElement(By.Name("remove")).Click();
    }
}