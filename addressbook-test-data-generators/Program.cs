using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Microsoft.CSharp;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddressbookTests;
using Formatting = Newtonsoft.Json.Formatting;


namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            // программа принимает три параметра
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];

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

            if (format == "excel")
            {
                // в качестве параметра передаем название файла который мы получили как параметр при запуске
                writeGroupsToExcelFile(groups, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format " + format);
                }

                writer.Close();
            }
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

        static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            //запуск excel через com interface
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = (Excel.Worksheet)wb.ActiveSheet;
            sheet.Cells[1, 1] = "test";
            // чтобы записать инфу о группах -> цикл
            //номер строки
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            // полный путь к файлу чтобы excel сохранил куда нужно
            // придварительно удаляем файл, чтобы при повторном создании он сохранил в тоже место
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

            // первый параметр - список элементов типа GroupData, второй куда будем записывать
            static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
            {
                //указываем какого типа данные он будет сериализовывать
                //первый параметр - куда, второй - что
                new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
            }

            static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
            {
                writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
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



