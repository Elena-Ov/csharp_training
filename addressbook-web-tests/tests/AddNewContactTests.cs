using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
   [TestFixture]
   public class AddNewContactTests : AuthTestBase
   {
       [Test]
       public void TheAddNewContactTest()
       {   
           ContactForm personalData = new ContactForm("", "");
           personalData.Firstname = "Ivan";
           personalData.Lastname = "Ivanov";
           
           app.Contact.CreateContact(personalData);
       }
       [Test]
       public void TheEmptyContactTest()
       {   
           ContactForm personalData = new ContactForm("", "");
           personalData.Firstname = "";
           personalData.Lastname = "";
           
           app.Contact.CreateContact(personalData);
       }
   }
}