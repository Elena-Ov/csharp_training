using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

[TestFixture]

public class ContactModificationTests : ContactTestBase
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

        List<ContactFormData> oldContacts = ContactFormData.GetAllContacts();
        ContactFormData contactToBeModified = oldContacts[0];
        app.Contact.ModifyContacts(contactToBeModified, modifiedPersonalData);
        //убеждаемся что размер старого и нового списков совпадают
        Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

        
        List<ContactFormData> newContacts = ContactFormData.GetAllContacts();
        oldContacts[0].Lastname = modifiedPersonalData.Lastname;
        oldContacts[0].Firstname = modifiedPersonalData.Firstname;
        oldContacts.Sort();
        newContacts.Sort();
        Assert.AreEqual(oldContacts, newContacts);
        foreach (ContactFormData contact in newContacts)
        {
            if (contact.Id == contactToBeModified.Id)
            {
                Assert.AreEqual(modifiedPersonalData.Lastname, contact.Lastname); 
                Assert.AreEqual(modifiedPersonalData.Firstname, contact.Firstname);
            }
        }
    }
}