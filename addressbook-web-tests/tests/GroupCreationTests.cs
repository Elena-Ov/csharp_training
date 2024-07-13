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
            //List - контейнер или коллекция, объект, который хранит набор других объектов
            // получаем список групп до создания новой
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.CreateGroup(group);
            // получаем список групп после создания новой
            List<GroupData> newGroups = app.Groups.GetGroupList(); // возвращать будет список объектов типа GroupData
            // проверяем что новый список на 1 длинее
            Assert.AreEqual(oldGroups.Count +1, newGroups.Count);
        }
        [Test]
        public void EmptyGroupCreationTest()
        {   
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.CreateGroup(group);
           
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            Assert.AreEqual(oldGroups.Count +1, newGroups.Count);
        }
        /*[Test] 
        public void BadNameGroupCreationTest()
        {   
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.CreateGroup(group);
           
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            Assert.AreEqual(oldGroups.Count +1, newGroups.Count);
        }*/
    }
}
