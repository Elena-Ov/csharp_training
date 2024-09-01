using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace AddressbookTestsWhite
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            ContactData contact = new ContactData("White", "Floppy");

            app.Contacts.AddNewContact(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }
    }
}