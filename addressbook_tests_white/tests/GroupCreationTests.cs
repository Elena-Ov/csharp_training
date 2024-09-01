using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookTestsWhite
{
    [TestFixture]
    public class GroupCreationTests : TestBase
{
    [Test]
    public void TestGroupCreation()
    {
        List<GroupData> oldGroups = app.Groups.GetGroupList();
        GroupData newGroup = new GroupData()
        {
            Name = "test_white"
        };
        app.Groups.AddGroup(newGroup);
        List<GroupData> newGroups = app.Groups.GetGroupList();
        oldGroups.Add(newGroup);
        oldGroups.Sort();
        newGroups.Sort();
        
        Assert.AreEqual(oldGroups, newGroups);
    }
}
    
}
