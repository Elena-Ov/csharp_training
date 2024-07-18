using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {   
            GroupData group = new GroupData("mmm");
            group.Header = "ggg";
            group.Footer = "nnn";
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.CreateGroup(group);
            
            // операция которая быстро вернет количество групп не читая их названия
            // тк переменная локальная, то подставляем сразу в проверку обращение к методу
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
           
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            
            Assert.AreEqual(oldGroups, newGroups);
        }
        [Test]
        public void EmptyGroupCreationTest()
        {   
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.CreateGroup(group);
            
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
        /*[Test] 
        public void BadNameGroupCreationTest()
        {   
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.CreateGroup(group);
           
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList(); 
           oldGroups.Add(group);
           oldGroups.Sort();
           newGroups.Sort();
           Assert.AreEqual(oldGroups.Count +1, newGroups.Count);
        }*/
    }
}
