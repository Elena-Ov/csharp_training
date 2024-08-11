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
        GroupData newData = new GroupData("ok"); 
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
        
        List<GroupData> oldGroups = app.Groups.GetGroupList();
        // запоминаем инфу для сравнения идентификаторов заранее
        // тк список после модификации сортируется, имена по индексу не совпадут
        GroupData oldData = oldGroups[0]; 
        app.Groups.ModifyGroup(0, newData);
        // размер старого и нового списка совпадает
        Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
        
        List<GroupData> newGroups = app.Groups.GetGroupList();
        oldGroups[0].Name = newData.Name;
        oldGroups.Sort();
        newGroups.Sort();
        Assert.AreEqual(oldGroups, newGroups);
        // проверяем что модифицировался нужный элемент (в списке был на первом месте)
        // пробегаемся по всем элементам и находим тот у которого нужный id
        // проверяем что его имя стало таким каким должно
        foreach (GroupData group in newGroups)
        {
            if (group.Id == oldData.Id)
            {
                Assert.AreEqual(newData.Name, group.Name); 
            }
        }
    }
}




