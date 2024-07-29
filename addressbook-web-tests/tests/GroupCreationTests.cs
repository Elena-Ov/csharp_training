using System;
using System.IO;
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
        // занятие 6.1
        public static IEnumerable<GroupData> GroupDataFromFile()
        {
            //создаем список, который в конце возвращаем
            List<GroupData> groups = new List<GroupData>();
            // читаем данные из файла, возвращаемое значение это массив строк, по которому мы устраиваем цикл
            string[] lines = File.ReadAllLines(@"groups1.csv");
            foreach (string l in lines)
            {
                // разбиваем строку на кусочки
                string[] parts = l.Split(',');
                // создаем новый объект и добавляем его в список Group
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }
        
        //привязываем тест к генератору
        // первый генератор
        //[Test, TestCaseSource("RandomGroupDataProvider")]
        // второй генератор
        [Test, TestCaseSource("GroupDataFromFile")]
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
