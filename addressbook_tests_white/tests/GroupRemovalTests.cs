using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace AddressbookTestsWhite
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int indexOfRemovedGroup = 1;
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            while (oldGroups.Count <= indexOfRemovedGroup)
            {
                app.Groups.AddGroup(new GroupData("NewTestGroup"));
                oldGroups = app.Groups.GetGroupList();
            }

            GroupData TobeRemoved = oldGroups[indexOfRemovedGroup];

            app.Groups.RemoveGroup(TobeRemoved);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Remove(TobeRemoved);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}