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
        GroupData newData = new GroupData("jjj"); 
        newData.Header = null; 
        newData.Footer = null;
        
        app.Groups.manager.Navigator.GoToGroupsPage();
        if (!app.Groups.IsGroupFound())
        {
            GroupData group = new GroupData("nnn");
            group.Header = "fff"; 
            group.Footer = "ggg";
            
            app.Groups.CreateGroup(group);
        }
        
        app.Groups.ModifyGroup(1, newData);
    }
}




