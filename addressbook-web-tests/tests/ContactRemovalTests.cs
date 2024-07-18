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
            personalData.Firstname = "Leo";
            personalData.Lastname = null;

            app.Contact.CreateContact(personalData);
        }
        List<ContactForm> oldContacts = app.Contact.GetContactsList();
        app.Contact.RemovePersonalData(1);
        
        List<ContactForm> newContacts = app.Contact.GetContactsList();
        oldContacts.RemoveAt(0);
        Assert.AreEqual(oldContacts, newContacts);
        
    }
}
