using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
   [TestFixture]
   public class AddNewContactTest : TestBase
   {
       [Test]
       public void TheAddNewContactTest()
       {   
           app.Navigator.OpenHomePage();
           app.Auth.Login(new AccountData("admin", "secret"));
           app.Contact.AddNewPage();
           app.Contact.NewContactFillinForm(new ContactForm("Mike", "Orlov"));
           app.Contact.SubmitContactForm();
           app.Contact.ReturnToHomePage();
           app.Auth.LogOut();
       }
   }
}