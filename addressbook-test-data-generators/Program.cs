using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;


namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            // передаем количество тестовых данных которые хотим сгенерировать
            int count = Convert.ToInt32(args[0]);
            
            // лекция 6.2 - добавляем 3-й параметр - формат
            //StreamWriter writer = new StreamWriter(args[1]);
            //string format = args[3];
            
            // создаем новый объект
            StreamWriter writer = new StreamWriter(args[1]);
            for (int i = 0; i < count; i++)
            {
                // записываем три значения разделенные запятыми
                // для этого используем форматирование
                writer.WriteLine(String.Format("${0},${1},${2}",
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10)));
            }
            //writeGroupsToCsvFile(groups, writer);
            writer.Close();
        }
        // чтобы писать данные в разных форматах определяем функции которые это будут делать
       /* static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }
        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer){}*/
    }
}

