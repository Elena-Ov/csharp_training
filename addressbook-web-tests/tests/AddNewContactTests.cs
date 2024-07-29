using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
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
               {
                   //Firstname = GenerateRandomString(100),
                   //Lastname = GenerateRandomString(100)
               });
           }
           return contactsPersonalData;
       }
       
       [Test, TestCaseSource("RandomContactDataProvider")]
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