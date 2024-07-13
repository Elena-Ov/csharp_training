using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.manager.Navigator.GoToGroupsPage();
            
            if (!app.Groups.IsGroupFound())
            {
                GroupData group = new GroupData("abc"); 
                group.Header = "fff"; 
                group.Footer = "ggg";
                
                app.Groups.CreateGroup(group);
            }
            // читаем список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.RemoveGroup(0);
            
            // после удаления извлекаем новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            oldGroups.RemoveAt(0);
            // сравниваем старый список полученный до вызова тестируемой функции с удаленным элементом
            // с новым списком полученным после выполнения тестируемой операции 
            Assert.AreEqual(oldGroups, newGroups);
        }
        }
    }

    