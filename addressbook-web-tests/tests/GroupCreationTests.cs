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
            GroupData group = new GroupData("mmm");
            group.Header = "ggg";
            group.Footer = "nnn";
            
            app.Groups.Create(group);
        }
        [Test]
        public void EmptyGroupCreationTest()
        {   
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            
            app.Groups.Create(group);
        }
    }
}
