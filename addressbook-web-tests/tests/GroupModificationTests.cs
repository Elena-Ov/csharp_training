using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

[TestFixture]
public class GroupModificationTests : AuthTestBase
{
    [Test]
    public void GroupModificationTest()
    {
        // prepare
        GroupData newData = new GroupData("abc"); // создание объекта типа GroupData
        newData.Header = null; // в поле остается прежнее значение, с ним не выполняется никаких действий
        newData.Footer = null;
        // action 
        app.Groups.EitherModifyOrCreateGroup(newData, 1);
        // verification
        Assert.IsTrue(app.Groups.IsGroupFound()); 
    }
}

