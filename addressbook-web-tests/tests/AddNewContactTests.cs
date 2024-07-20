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
       [Test]
       public void TheAddNewContactTest()
       {
           ContactForm personalData = new ContactForm("", "");
           personalData.Firstname = "Ron";
           personalData.Lastname = "Kon";
           // получаем список контактов до создания новых
           List<ContactForm> oldContacts = app.Contact.GetContactsList();
           // создаем новый контакт
           app.Contact.CreateContact(personalData);
           // операция которая быстро вернет количество контактов не читая их названия
           // если кол-во совпало переходим к проверке содержимого
           Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

           // получаем список контактов после создания
           List<ContactForm> newContacts = app.Contact.GetContactsList();
           // к старому списку добавляем новый контакт
           oldContacts.Add(personalData);
           // упорядочиваем списки
           oldContacts.Sort();
           newContacts.Sort();
           // сравниваем старый список контактов с добавленным контактом и новый список контактов
           Assert.AreEqual(oldContacts, newContacts);
       }
        
       [Test]
       public void TheEmptyContactTest()
       {   
           ContactForm personalData = new ContactForm("", "");
           personalData.Firstname = "";
           personalData.Lastname = "";
           
           List<ContactForm> oldContacts = app.Contact.GetContactsList();
           app.Contact.CreateContact(personalData);
           Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());
           List<ContactForm> newContacts = app.Contact.GetContactsList();
           oldContacts.Add(personalData);
           oldContacts.Sort();
           newContacts.Sort();
           Assert.AreEqual(oldContacts, newContacts);
       }
   }
}