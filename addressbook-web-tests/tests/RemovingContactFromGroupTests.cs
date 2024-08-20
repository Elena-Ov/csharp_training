using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests

{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            //выбираем группу
            GroupData group = GroupData.GetAll()[0];
            //сохраняем старый список
            List<ContactFormData> oldList = group.GetContactsByGroup();
            //выбираем первый контакт
            ContactFormData contact = ContactFormData.GetAllContacts().First();
            //actions
            app.Contact.RemoveContactFromGroup(contact, group);

            List<ContactFormData> newList = group.GetContactsByGroup();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();
            
            Assert.AreEqual(oldList, newList);
        }
    
    }   
}