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
            ContactForm personalData = new ContactForm("", "");
            personalData.Firstname = "Gigi";
            personalData.Lastname = "Ban";

            app.Contact.CreateContact(personalData);
        }
        List<ContactForm> oldContacts = app.Contact.GetContactsList();
        app.Contact.RemovePersonalData(1);
        Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

        
        List<ContactForm> newContacts = app.Contact.GetContactsList();
        ContactForm toBeRemoved = oldContacts[1];
        oldContacts.RemoveAt(1);
        Assert.AreEqual(oldContacts, newContacts);
        
        foreach (ContactForm contact in newContacts)
        {
            Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
        }
        
    }
}
