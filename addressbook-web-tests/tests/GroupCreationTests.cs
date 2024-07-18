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
            // создаем
            app.Groups.CreateGroup(group);
           // получаем список после создания
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            
            // к старому списку добавляем группу которую мы только что создали в нашей адресной книге
            oldGroups.Add(group);
            // перед сравнением упорядочиваем списки одинаково
            oldGroups.Sort();
            newGroups.Sort();
            // сравниваем старый список с этой добавленной группой и новый список прочитанный из приложения 
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
           
            List<GroupData> newGroups = app.Groups.GetGroupList(); 
           oldGroups.Add(group);
           oldGroups.Sort();
           newGroups.Sort();
           Assert.AreEqual(oldGroups.Count +1, newGroups.Count);
        }*/
    }
}
