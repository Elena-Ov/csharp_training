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
            // prepare
            GroupData oldData = new GroupData("abc"); // создание объекта типа GroupData
            oldData.Header = "fff"; 
            oldData.Footer = "ggg";
            app.Groups.EitherCreateOrRemoveGroup(oldData);
            // action 
            app.Groups.RemoveGroup(1);
            /* verification
            Assert.IsTrue(app.Groups.IsGroupDeleted());*/
        }
    }
}