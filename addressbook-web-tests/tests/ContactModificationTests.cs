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
        modifiedPersonalData.Firstname = "Lion";
        modifiedPersonalData.Lastname = null;
        
        app.Contact.manager.Navigator.OpenHomePage();
        if (app.Contact.IsContactFound())
        {
            app.Contact.ModifyContacts(2, modifiedPersonalData);
        }
        else
        {
            ContactForm personalData = new ContactForm("", "");
            personalData.Firstname = "Volf";
            personalData.Lastname = null;
            
            app.Contact.CreateContact(personalData);
            app.Contact.ModifyContacts(2, modifiedPersonalData);
        }
    }
}