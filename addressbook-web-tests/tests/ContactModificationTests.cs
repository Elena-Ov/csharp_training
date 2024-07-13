using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

[TestFixture]

public class ContactModificationTests : AuthTestBase
{
    [Test]
    public void ContactModificationTest()
    { 
        ContactForm modifiedPersonalData = new ContactForm("", "");
        modifiedPersonalData.Firstname = "Bear";
        modifiedPersonalData.Lastname = null;
        
        app.Contact.manager.Navigator.OpenHomePage();
        if (!app.Contact.IsContactFound())
        {
            ContactForm personalData = new ContactForm("", "");
            personalData.Firstname = "Volf";
            personalData.Lastname = null;
            
            app.Contact.CreateContact(personalData);
        }

        app.Contact.ModifyContacts(2, modifiedPersonalData);
    }
}