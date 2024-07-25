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
        //получаем информацию об отдельно взятом контакте из таблицы
        ContactFormData fromTable = app.Contact.GetContactInformationFromTable(0);
        // получаем информацию об отдельно взятом контакте из формы
        ContactFormData fromForm = app.Contact.GetContactInformationFromEditForm(0);
        //verification, сравниваем имя и фамилию и т.д.
        Assert.AreEqual(fromTable, fromForm);
        Assert.AreEqual(fromTable.Address, fromForm.Address);
        Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

    }
}