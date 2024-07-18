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

public class GroupHelper : HelperBase
{
    protected string baseURL = "http://localhost";

    public GroupHelper(ApplicationManager manager) : base(manager) // в базовый класс передаем тоже ссылку на manager
    {
    }

    public GroupHelper CreateGroup(GroupData group)
    {
        manager.Navigator
            .GoToGroupsPage(); // GroupHelper обращается к manager чтобы он выдал navigator и он смог перейти на страницу
        InitGroupCreation();
        FillGroupForm(group);
        SubmitGroupCreation();
        ReturnToGroupsPage();
        return this; // когда вызываем в GroupHelper метод, то возвращается ссылка на него же самого
    }

    public GroupHelper ModifyGroup(int p, GroupData newData)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(p);
        InitGroupModification();
        FillGroupForm(newData);
        SubmitGroupModification();
        ReturnToGroupsPage();
        return this;
    }
    public GroupHelper RemoveGroup(int p)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(p);
        RemoveGroupButton();
        ReturnToGroupsPage();
        return this;
    }

    public GroupHelper InitGroupCreation()
    {
        driver.FindElement(By.Name("new")).Click();
        return this;
    }

    public GroupHelper FillGroupForm(GroupData group)
    {
        Type(By.Name("group_name"), group.Name);
        Type(By.Name("group_header"), group.Header);
        Type(By.Name("group_footer"), group.Footer);
        return this;
    }

    public GroupHelper SubmitGroupCreation()
    {
        driver.FindElement(By.Name("submit")).Click();
        groupCache = null; // очистка старого кеша
        return this;
    }

    public GroupHelper ReturnToGroupsPage()
    {
        driver.FindElement(By.LinkText("group page")).Click();
        return this;
    }

    public GroupHelper SelectGroup(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form/span[" + (index+1) + "]/input")).Click();
        //driver.FindElement(By.XPath("//input[@name='selected[]'])[" + (index+1) + "]")).Click();
        return this;
    }

    public GroupHelper RemoveGroupButton()
    {
        driver.FindElement(By.Name("delete")).Click();
        groupCache = null; //очистка кеша
        return this;
    }

    public GroupHelper InitGroupModification()
    {
        driver.FindElement(By.Name("edit")).Click();
        return this;
    }

    public GroupHelper SubmitGroupModification()
    {
        driver.FindElement(By.Name("update")).Click();
        groupCache = null; // очистка кеша
        return this;
    }

    public bool IsGroupFound() // проверка есть ли хотя бы одна группа 
    {
        return driver.Url == baseURL + "/addressbook/group.php" &&
               IsElementPresent(By.Name("selected[]"));
    }

    // создаем приватное поле типа List<GroupData>
    // здесь будет хранится заполненный и сохраненный список групп
    // в самом начале он пустой
    private List<GroupData> groupCache = null;
    public List<GroupData> GetGroupList()
    {
        // при !первом обращении когда выполняется метод GetGroupList() мы будем этот список заполнять
        // при повторном мы будем использовать ранее заполненный список -> проверка

        if (groupCache == null) // заходим в блок, если groupCache != null, то сразу return groupCache;
        {
            groupCache = new List<GroupData>();
            manager.Navigator.GoToGroupsPage(); 
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            foreach (IWebElement element in elements)
            {
                groupCache.Add(new GroupData(element.Text));
            }
        }
        // кеш вернуть прямой ссылкой нельзя, возвращаем копию
        // новый список построенный из старого
        return new List<GroupData>(groupCache);
    }
}


