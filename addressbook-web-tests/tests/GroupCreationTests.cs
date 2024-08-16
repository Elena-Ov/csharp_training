using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Microsoft.CSharp;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
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
        // занятие 6.4
       /* public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            //создаем новый список, в который будем читать данные
            List<GroupData> groups = new List<GroupData>();
            //создаем приложение
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups1.xlsx"));
            Excel.Worksheet sheet = (Excel.Worksheet)wb.ActiveSheet;
            // прямоугольник к-й содержит данные
            Excel.Range range = sheet.UsedRange;
            // значение номеров строк меняется от 1 до полного совпадения с количеством строк
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    // добавляем объекты, к-е заполняются значениями из таблицы
                    // 1,2,3 - ячейки
                    Name = range.Cells[i, 1].Value2,
                    Header = range.Cells[i, 2].Value2,
                    Footer = range.Cells[i, 3].Value2

                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }*/

        //привязываем тест к генератору
        // первый генератор
        //[Test, TestCaseSource("RandomGroupDataProvider")]
        // генераторы csv, xml, json, excel
        //[Test, TestCaseSource("GroupDataFromCsvFile")]
        //[Test, TestCaseSource("GroupDataFromXmlFile")]
        [Test, TestCaseSource("GroupDataFromJsonFile")]
        //[Test, TestCaseSource("GroupDataFromExcelFile")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.CreateGroup(group);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivity()
        { 
            // проверяем работоспособность методов
            //извлекаем все группы, потом 0-ю, у нее получаем список контактов
            // выводим на консоль
            foreach (ContactFormData contact in GroupData.GetAll()[0].GetContactsByGroup())
            {
                System.Console.Out.WriteLine();
            }

            /*
            // сравним скорость извлечения из UI и БД
            DateTime start = DateTime.Now;
            //через web интерфейс
            List<GroupData> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            // из временной ветки которая соответствует концу вычитаем начала
            System.Console.Out.WriteLine(end.Subtract(start));

            // из БД
            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start)); */
        }
    }
}
