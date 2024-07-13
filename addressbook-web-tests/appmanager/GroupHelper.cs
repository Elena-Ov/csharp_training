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
        return this;
    }

    public bool IsGroupFound() // проверка есть ли хотя бы одна группа 
    {
        return driver.Url == baseURL + "/addressbook/group.php" &&
               IsElementPresent(By.Name("selected[]"));
    }

    public List<GroupData> GetGroupList()
    {
        List<GroupData> groups = new List<GroupData>();
        manager.Navigator.GoToGroupsPage(); // идем на нужную стр
        // читаем список на стр
        // испотльзуем метод который вернет все найденные элементы
        // сохраняем список в переменную elements
        // прописываем более общий тип ICollection
        ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
        // чтобы превратить полученные элементы в нужные нам элементы типа GroupData используем цикл
        foreach (IWebElement element in elements)
        {
            /*создаем новый объект типа GroupData и element.Text используем в качестве параметра
            GroupData group = new GroupData(element.Text); 
            после создания помещаем этот объект в 
            groups.Add(group);*/
            groups.Add(new GroupData(element.Text));
        }
        return groups;
    }
}


