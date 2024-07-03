using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;
[TestFixture]
public class LoginTests : TestBase
{
    [Test]
    public void LoginWithValidCredentials()
    {
        //prepare
        app.Auth.LogOut();
        //action
        AccountData account = new AccountData("admin", "secret");
        app.Auth.Login(account);
        //verification
        Assert.IsTrue(app.Auth.IsLoggedIn());
    }
    [Test]
    public void LoginWithInvalidCredentials()
    {
        //prepare
        app.Auth.LogOut();
        //action
        AccountData account = new AccountData("admin", "123456");
        app.Auth.Login(account);
        //verification
        Assert.IsFalse(app.Auth.IsLoggedIn());
    }
}