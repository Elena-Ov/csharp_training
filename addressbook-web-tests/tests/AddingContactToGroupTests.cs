using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests

{
    public class AddingContactToGroupTests : AuthTestBase
{
    [Test]
    public void TestAddingContactToGroup()
    {
        // выбираем группу
        GroupData group = app.Groups.GetGroup();
        //выбираем контакт
        ContactFormData contact = app.Contact.GetContact(group);
        // запоминаем старый список контактов
        List<ContactFormData> oldList = group.GetContactsByGroup();
        // actions
        app.Contact.AddContactToGroup(contact, group);

        List<ContactFormData> newList = group.GetContactsByGroup();
        oldList.Add(contact);
        newList.Sort();
        oldList.Sort();
        
        Assert.AreEqual(oldList, newList);
    }
    
}

}
