using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
    {
        //создаем список, который в конце возвращаем
        List<GroupData> groups = new List<GroupData>();
        // читаем данные из файла, возвращаемое значение это массив строк, по которому мы устраиваем цикл
        string[] lines = File.ReadAllLines(@"groups1.csv");
        foreach (string l in lines)
        {
            // разбиваем строку на кусочки, разделитель по желанию
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
        // занятие 6.2
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            // читаем данные из файла, возвращаемое значение это массив строк, по которому мы устраиваем цикл
            // приведение типа, т.к. метод Deserialize возвращает какой-то обсрактный объект
            // нужно указать компилятору явно что мы знаем какого типа этот объект
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                    .Deserialize(new StreamReader(@"groups1.xml"));
        }
        // занятие 6.3
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        { 
            // указываем какого типа должен быть объект - <List<GroupData>
            // в качестве параметра передаем текст прочитанный из файла
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups1.json"));
        }

        //привязываем тест к генератору
        // первый генератор
        //[Test, TestCaseSource("RandomGroupDataProvider")]
        // генераторы csv, xml, json
        [Test, TestCaseSource("GroupDataFromCsvFile")]
        //[Test, TestCaseSource("GroupDataFromXmlFile")]
        //[Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.CreateGroup(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
