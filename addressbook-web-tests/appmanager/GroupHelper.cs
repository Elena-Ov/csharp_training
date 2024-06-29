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
    public GroupHelper(ApplicationManager manager) : base(manager) // в базовый класс передаем тоже ссылку на manager
    {
    }

    public GroupHelper Create(GroupData group)
    {
        manager.Navigator.GoToGroupsPage();// GroupHelper обращается к manager чтобы он выдал navigator и он смог перейти на страницу
        InitGroupCreation();
        FillGroupForm(group);
        SubmitGroupCreation();
        ReturnToGroupsPage();
        return this; // когда вызываем в GroupHelper метод, то возвращается ссылка на него же самого
    }
    public GroupHelper Modify(int p, GroupData newData)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(p);
        InitGroupModification();
        FillGroupForm(newData);
        SubmitGroupModification();
        ReturnToGroupsPage();
        return this;
    }
    public GroupHelper Remove(int p)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(p);
        RemoveGroup();
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
        driver.FindElement(By.Name("group_name")).Click();
        driver.FindElement(By.Name("group_name")).Clear();
        driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        driver.FindElement(By.Name("group_header")).Click();
        driver.FindElement(By.Name("group_header")).Clear();
        driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
        driver.FindElement(By.Name("group_footer")).Click();
        driver.FindElement(By.Name("group_footer")).Clear();
        driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
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
        driver.FindElement(By.XPath("//div[@id='content']/form/span["+ index +"]/input")).Click();
        return this;
    }
    public GroupHelper RemoveGroup()
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
}