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
        // prepare
        ContactForm modifiedPersonalData = new ContactForm("", "");
        modifiedPersonalData.Firstname = "Lion";
        modifiedPersonalData.Lastname = null;
        // action 
        app.Contact.EitherModifyOrCreateContact(2, modifiedPersonalData);
        // verification
        Assert.IsTrue(app.Contact.IsContactFound()); 
    }
}