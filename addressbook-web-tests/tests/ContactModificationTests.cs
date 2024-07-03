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

        app.Contact.ModifyContacts(2, modifiedPersonalData);
    }
}