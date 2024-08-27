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

    //метод изменяющий группу по порядковому номеру
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
    //метод изменяющий группу по уникальному идентификатору
    public GroupHelper ModifyGroup(GroupData group, GroupData newData)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(group.Id);
        InitGroupModification();
        FillGroupForm(newData);
        SubmitGroupModification();
        ReturnToGroupsPage();
        return this;
    }

    //метод удаляющий группу по порядковому номеру
    public GroupHelper RemoveGroup(int p)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(p);
        RemoveGroupButton();
        ReturnToGroupsPage();
        return this;
    }
    
    //метод удаляющий группу по уникальному идентификатору
    public GroupHelper RemoveGroup(GroupData group)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(group.Id);
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

    // выбираем по индексу
    public GroupHelper SelectGroup(int index)
    {
        driver.FindElement(By.XPath("//div[@id='content']/form/span[" + (index + 1) + "]/input")).Click();
        //driver.FindElement(By.XPath("//input[@name='selected[]'])[" + (index+1) + "]")).Click();
        return this;
    }
    
    // выбираем по идентификатору
    public GroupHelper SelectGroup(string id)
    {
        driver.FindElement(By.XPath("//input[@name='selected[]' and @value='"+id+"']")).Click();
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
                // на первом проходе заполняем только идентификаторы
                groupCache.Add(new GroupData(null)
                {
                    Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                });
            }

            // далее
            string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
            // режем строку на много кусочков, в качестве параметра передаем разделитель
            string[] parts = allGroupNames.Split('\n');
            // определяем величину сдвига
            // на сколько в кеше правильных групп больше чем мы смогли получить
            int shift = groupCache.Count - parts.Length;
            // прописваем полученные имена в ранее созданные группы
            // перебираем элементы по индексу
            for (int i = 0; i < groupCache.Count; i++)
            {
                if (i < shift)
                {
                    // прописываем пустое имя
                    groupCache[i].Name = "";
                }
                else //прописываем имя которое нам нужно, но со сдвигом
                {
                    groupCache[i].Name = parts[i - shift].Trim(); // Trim() удаляем лишние пробелы в начале и конце имени группы}
                }
            }
        }
        return new List<GroupData>(groupCache);
    }
    
public int GetGroupCount()        
    {
        return driver.FindElements(By.CssSelector("span.group")).Count;
    }
    
    //поиск или создание группы
    public GroupData GetGroup()
    {
        //получаем список всех групп из базы
        List<GroupData> groups = GroupData.GetAll();
        GroupData group;
        //если есть хоть одна группа - возвращаем первую попавшуюся
        if (groups.Any())
            group = groups[0];
        else //иначе создаём новую
        {
            group = new GroupData(TestBase.GenerateRandomString(50));
            //создаём группу и возвращаемся на страничку групп
            CreateGroup(group);
            //выбираем первую
            SelectGroup(0);
            group.Id = driver.FindElement(By.XPath("//div[@id='content']/form/span[1]/input")).GetAttribute("id");
        }
    
        return group;
    }
}


