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
            personalData.Firstname = "Lion";
            personalData.Lastname = null;

            app.Contact.CreateContact(personalData);
        }
        
        app.Contact.RemovePersonalData(2);
        
    }
}
