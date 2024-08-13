using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;
[TestFixture]

public class ContactRemovalTests : AuthTestBase
{
    [Test]
    public void ContactRemovalTest()
    {
        app.Contact.manager.Navigator.OpenHomePage();
        if (!app.Contact.IsContactFound())
        {
            ContactFormData personalData = new ContactFormData("", "");
            personalData.Firstname = "Gigi";
            personalData.Lastname = "Ban";

            app.Contact.CreateContact(personalData);
        }
        List<ContactFormData> oldContacts = ContactFormData.GetAllContacts();
        ContactFormData contactToBeRemoved = oldContacts[0];
        app.Contact.RemovePersonalData(contactToBeRemoved);
        app.Contact.manager.Navigator.OpenHomePage();
        //убеждаемся что размер уменьшился на 1 по сравнению со старым
        Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

        
        List<ContactFormData> newContacts = ContactFormData.GetAllContacts();
        
        oldContacts.RemoveAt(0);
        Assert.AreEqual(oldContacts, newContacts);
        
        foreach (ContactFormData contact in newContacts)
        {
            Assert.AreNotEqual(contact.Id, contactToBeRemoved.Id);
        }
        
    }
}
