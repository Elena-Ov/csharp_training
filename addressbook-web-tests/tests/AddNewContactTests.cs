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
           personalData.Firstname = "Ivan";
           personalData.Lastname = "Ivanov";
           // получаем список контактов до создания новых
           List<ContactForm> oldContacts = app.Contact.GetContactsList();
           // создаем новый контакт
           app.Contact.CreateContact(personalData);
       }
       /*public void TheAddNewContactTest()
       {   
           ContactForm personalData = new ContactForm("", "");
           personalData.Firstname = "Ivan";
           personalData.Lastname = "Ivanov";
           // получаем список контактов до создания новых
           List<ContactForm> oldContacts = app.Contact.GetContactsList();
           // создаем новый контакт
           app.Contact.CreateContact(personalData);
           // получаем список контактов после создания
           List<ContactForm> newContacts = app.Contact.GetContactsList();
           // к старому списку добавляем новый контакт
           oldContacts.Add(personalData);
           // упорядочиваем списки
           oldContacts.Sort();
           newContacts.Sort();
           // сравниваем старый список контактов с добавленным контактом и новый список контактов
           Assert.AreEqual(oldContacts, newContacts);
       }*/
       [Test]
       public void TheEmptyContactTest()
       {   
           ContactForm personalData = new ContactForm("", "");
           personalData.Firstname = "";
           personalData.Lastname = "";
           
           List<ContactForm> oldContacts = app.Contact.GetContactsList();
           app.Contact.CreateContact(personalData);
           List<ContactForm> newContacts = app.Contact.GetContactsList();
           oldContacts.Add(personalData);
           oldContacts.Sort();
           newContacts.Sort();
           Assert.AreEqual(oldContacts, newContacts);
       }
   }
}