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
        int IndexOfTheChosenContact = 0;
        app.Contact.FindOrCreateContact(IndexOfTheChosenContact);
        
        //получаем информацию об отдельно взятом контакте из таблицы
        ContactFormData fromTable = app.Contact.GetContactInformationFromTable(IndexOfTheChosenContact);
        // получаем информацию об отдельно взятом контакте из формы
        ContactFormData fromForm = app.Contact.GetContactInformationFromEditForm(IndexOfTheChosenContact);
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
        int IndexOfTheChosenContact = 0;
        app.Contact.FindOrCreateContact(IndexOfTheChosenContact);
        
        ContactFormData fromForm = app.Contact.GetContactInformationFromEditForm(IndexOfTheChosenContact);
       
        string lineFromEditForm = fromForm.GetInformationFromIdPage();
        
        string linefromIdPage = app.Contact.GetContactInformationFromIdPage(IndexOfTheChosenContact);
        
        Assert.AreEqual(lineFromEditForm, linefromIdPage);
    }
}