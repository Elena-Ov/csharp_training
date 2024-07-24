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
        ContactFormData modifiedPersonalData = new ContactFormData("", "");
        modifiedPersonalData.Firstname = "Jin";
        modifiedPersonalData.Lastname = "Jinov";
        
        app.Contact.manager.Navigator.OpenHomePage();
        if (!app.Contact.IsContactFound())
        {
            ContactFormData personalData = new ContactFormData("", "");
            personalData.Firstname = "Test";
            personalData.Lastname = "Testov";
            
            app.Contact.CreateContact(personalData);
        }

        List<ContactFormData> oldContacts = app.Contact.GetContactsList();
        app.Contact.ModifyContacts(1, modifiedPersonalData);
        //убеждаемся что размер старого и нового списков совпадают
        Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

        
        List<ContactFormData> newContacts = app.Contact.GetContactsList();
        oldContacts[0].Lastname = modifiedPersonalData.Lastname;
        oldContacts[0].Firstname = modifiedPersonalData.Firstname;
        oldContacts.Sort();
        newContacts.Sort();
        Assert.AreEqual(oldContacts, newContacts);
    }
}