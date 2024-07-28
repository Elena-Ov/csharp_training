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
        // реализуем метод генерации тестовых данных
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            // создаем список
            List<GroupData> groups = new List<GroupData>();
            //заполняем его данными, которые будем генерировать
            // 5 разных тестовых наборов
            for (int i = 0; i < 5; i++)
            {
                //максимальная длина строки, которую мы хотим сгенерировать
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }
        
        //привязываем тест к генератору
        [Test, TestCaseSource("RandomGroupDataProvider")]
        // информация о группе будет передаваться из вне
        public void GroupCreationTest(GroupData group)
        {   
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
