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
            if (!app.Groups.IsGroupFound())
            {
                GroupData group = new GroupData("abc"); 
                group.Header = "fff"; 
                group.Footer = "ggg";
                
                app.Groups.CreateGroup(group);
            }
            
            app.Groups.RemoveGroup(1);
        }
        }
    }

    