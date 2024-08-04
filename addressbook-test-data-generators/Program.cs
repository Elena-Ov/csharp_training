using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;


namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            // программа принимает три параметра
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[3];

            // формируем список
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            { 
                // создаем объекты и добавляем в список
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            if (format == "csv")
            {
                writeGroupsToCsvFile(groups, writer);
            }
            else if (format == "xml")
            {
                writeGroupsToXmlFile(groups, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized format " + format);
            }
            writer.Close();
        }
        // чтобы писать данные в разных форматах определяем функции которые это будут делать
        // comma-separated file - CSV
        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        // первый параметр - список элементов типа GroupData, второй куда будем записывать
        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            //первый параметр - куда, второй - что
           new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        /*{
            // запись текстовых файлов, передаем количество тестовых данных которые хотим сгенерировать
            int count = Convert.ToInt32(args[0]);

            // создаем новый объект
            StreamWriter writer = new StreamWriter(args[1]);
            for (int i = 0; i < count; i++)
            {
                // цикл для записи строчек в файл
                writer.WriteLine(String.Format("${0},${1},${2}",
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10)));
            }
            writer.Close();
        }*/
    }
}

