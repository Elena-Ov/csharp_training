using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

[TestFixture]
public class GroupModificationTests : GroupTestBase
{
    [Test]
    public void GroupModificationTest()
    {
        GroupData newData = new GroupData("hello"); 
        newData.Header = null; 
        newData.Footer = null;
        
        app.Groups.manager.Navigator.GoToGroupsPage();
        if (!app.Groups.IsGroupFound())
        {
            GroupData group = new GroupData("nnn");
            group.Header = "fff"; 
            group.Footer = "ggg";
            
            app.Groups.CreateGroup(group);
        }
        
        List<GroupData> oldGroups = GroupData.GetAll();
        // запоминаем инфу для сравнения идентификаторов заранее
        // тк список после модификации сортируется, имена по индексу не совпадут
        GroupData toBeModified = oldGroups[0]; 
        app.Groups.ModifyGroup(toBeModified, newData);
        // размер старого и нового списка совпадает
        Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
        
        List<GroupData> newGroups = GroupData.GetAll();
        oldGroups[0].Name = newData.Name;
        oldGroups.Sort();
        newGroups.Sort();
        Assert.AreEqual(oldGroups, newGroups);
        // проверяем что модифицировался нужный элемент (в списке был на первом месте)
        // пробегаемся по всем элементам и находим тот у которого нужный id
        // проверяем что его имя стало таким каким должно
        foreach (GroupData group in newGroups)
        {
            if (group.Id == toBeModified.Id)
            {
                Assert.AreEqual(newData.Name, group.Name); 
            }
        }
    }
}




