using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
   [TestFixture]
   public class AddNewContact : TestBase
   {
       [Test]
       public void TheAddNewContactTest()
       {   
           OpenHomePage();
           Login(new AccountData("admin", "secret"));
           AddNewPage();
           NewContactFillinForm(new ContactForm("Georg", "Petrof"));
           SubmitContactForm();
           ReturnToHomePage();
           LogOut();
       }
   }
}