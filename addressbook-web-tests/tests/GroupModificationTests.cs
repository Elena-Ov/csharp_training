using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("uuu");
            newData.Header = null;// в поле остается прежнее значение, с ним не выполняется никаких действий
            newData.Footer = null;
            
            app.Groups.Modify(1, newData);
        }
    }
