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
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.RemoveGroup(0);
            // убеждаемся что количество уменьшилось по сравнению со старым списком
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            oldGroups.RemoveAt(0);
            
            Assert.AreEqual(oldGroups, newGroups);
        }
        }
    }

    