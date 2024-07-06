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
        // prepare
        ContactForm oldPersonalData = new ContactForm("", "");
        oldPersonalData.Firstname = "Lion";
        oldPersonalData.Lastname = null;
        app.Contact.EitherCreateOrRemoveContact(oldPersonalData);
        // action 
        app.Contact.RemovePersonalData(2);
        //Assert.IsTrue(app.Contact.IsContactDeleted());
    }
}
