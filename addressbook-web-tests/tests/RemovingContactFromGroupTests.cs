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
            // выбираем группу
            GroupData group = app.Groups.GetGroup();
            //выбираем контакт
            ContactFormData contact = app.Contact.GetContact(group);
            //сохраняем старый список
            List<ContactFormData> oldList = group.GetContactsByGroup();
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