using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.manager.Navigator.GoToGroupsPage();
            if (app.Groups.IsGroupFound())
            {
                app.Groups.RemoveGroup(1);
            }
            else
            {
                
                GroupData group = new GroupData("abc"); // создание объекта типа GroupData
                group.Header = "fff"; 
                group.Footer = "ggg";
                
                app.Groups.CreateGroup(group);
                app.Groups.RemoveGroup(1);
            }
        }
        }
    }

    