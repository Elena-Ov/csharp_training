using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {   
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroupsPage();
            app.Groups.InitGroupCreation();
            GroupData group = new GroupData("tolpa");
            group.Footer = "trud";
            group.Header = "may";
            app.Groups.FillGroupForm(group);
            app.Groups.SubmitGroupCreation();
            app.Groups.ReturnToGroupsPage();
            app.Auth.LogOut();
        }
    }
}