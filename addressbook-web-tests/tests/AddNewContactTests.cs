using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Microsoft.CSharp;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace WebAddressbookTests
{
   [TestFixture]
   public class AddNewContactTests : AuthTestBase
   {
       public static IEnumerable<ContactFormData> RandomContactDataProvider()
       {
           // создаем список
           List<ContactFormData> contactsPersonalData = new List<ContactFormData>();
           //заполняем его данными, которые будем генерировать
           // 5 разных тестовых наборов
           for (int i = 0; i < 5; i++)
           {
               //максимальная длина строки, которую мы хотим сгенерировать
               contactsPersonalData.Add(new ContactFormData(GenerateRandomString(30), GenerateRandomString(50))
               { });
           }
           return contactsPersonalData;
       }
       
       // чтение xml файла
       public static IEnumerable<ContactFormData> ContactDataFromXmlFile()
       {
           return (List<ContactFormData>)
               new XmlSerializer(typeof(List<ContactFormData>))
                   .Deserialize(new StreamReader(@"contacts.xml"));
       }
       // чтение json файла
       public static IEnumerable<ContactFormData> ContactDataFromJsonFile()
       { 
           // указываем какого типа должен быть объект - <List<ContactFormData>
           // в качестве параметра передаем текст прочитанный из файла
           return JsonConvert.DeserializeObject<List<ContactFormData>>(
               File.ReadAllText(@"contacts.json"));
       }
       // чтение csv файла
       public static IEnumerable<ContactFormData> ContactDataFromCsvFile()
       {
           //создаем список, который в конце возвращаем
           List<ContactFormData> contactsPersonalData = new List<ContactFormData>();
           // читаем данные из файла, возвращаемое значение это массив строк, по которому мы устраиваем цикл
           string[] lines = File.ReadAllLines(@"contacts.csv");
           foreach (string l in lines)
           {
               // разбиваем строку на кусочки, разделитель по желанию
               string[] parts = l.Split(',');
               // создаем новый объект и добавляем его в список ContactFormData
               contactsPersonalData.Add(new ContactFormData(parts[0], parts[1])
               {
               });
           }

           return contactsPersonalData;
       }
       
       //[Test, TestCaseSource("RandomContactDataProvider")]
       [Test, TestCaseSource("ContactDataFromXmlFile")]
       //[Test, TestCaseSource("ContactDataFromJsonFile")]
       //[Test, TestCaseSource("ContactDataFromCsvFile")]
       public void TheAddNewContactTest(ContactFormData personalData)
       {
           // получаем список контактов до создания новых
           List<ContactFormData> oldContacts = app.Contact.GetContactsList();
           // создаем новый контакт
           app.Contact.CreateContact(personalData);
           // операция которая быстро вернет количество контактов не читая их названия
           // если кол-во совпало переходим к проверке содержимого
           Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

           // получаем список контактов после создания
           List<ContactFormData> newContacts = app.Contact.GetContactsList();
           // к старому списку добавляем новый контакт
           oldContacts.Add(personalData);
           // упорядочиваем списки
           oldContacts.Sort();
           newContacts.Sort();
           // сравниваем старый список контактов с добавленным контактом и новый список контактов
           Assert.AreEqual(oldContacts, newContacts);
       }
   }
}