using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests;

[TestFixture]

public class ContactInformationTests : AuthTestBase
{
    [Test]
    public void TestContactInformation()
    {
        int NumberOfContacts = 0;
        app.Contact.FindOrCreateContact(NumberOfContacts);
        
        //получаем информацию об отдельно взятом контакте из таблицы
        ContactFormData fromTable = app.Contact.GetContactInformationFromTable(NumberOfContacts);
        // получаем информацию об отдельно взятом контакте из формы
        ContactFormData fromForm = app.Contact.GetContactInformationFromEditForm(NumberOfContacts);
        //verification, сравниваем имя и фамилию и т.д.
        Assert.AreEqual(fromTable, fromForm);
        Assert.AreEqual(fromTable.Address, fromForm.Address);
        Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

    }
    // новое для проверки детальной информации о контакте
    [Test]
    public void TestDetailedContactInformation()
    {
        int NumberOfContacts = 0;
        app.Contact.FindOrCreateContact(NumberOfContacts);
        
        ContactFormData fromForm = app.Contact.GetContactInformationFromEditForm(NumberOfContacts);
       
        string lineFromEditForm = fromForm.GetInformationFromIdPage();
        
        string linefromIdPage = app.Contact.GetContactInformationFromIdPage(NumberOfContacts);
        
        if (linefromIdPage.IndexOf("Member of: ") >= 0)
        {
            linefromIdPage = linefromIdPage.Substring(0, linefromIdPage.IndexOf("Member of: "));
            while (linefromIdPage.Substring(linefromIdPage.Length - 2) == "\n")
            {
                linefromIdPage = linefromIdPage.Substring(linefromIdPage.Length - 2);
            }
        }
        Assert.AreEqual(lineFromEditForm, linefromIdPage);
    }
}