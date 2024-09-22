using NUnit.Framework;
using System.IO;

namespace MantisTests;

[TestFixture]
public class AccountCreationTests : TestBase
{
    [SetUp]
    //[TestFixtureSetUp]
    public void SetUpConfig()
    {
        //выполняем backup config файла
        app.Ftp.BackupFile("/config_inc.php");
        using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
        {
            // загружаем локальный файл
            app.Ftp.Upload("/config_inc.php", null);
        }
    }
   /* [Test]
    public void TestAccountRegistration()
    {
        List<AccountData> accounts = app.Admin.GetAllAccounts();
        AccountData account = new AccountData()
        {
            Name = "testuser",
            Password = "password",
            Email = "testuser@localhost.localdomain"
        };
        app.Admin.DeleteAccount(account);
        app.James.Delete(account);
        app.James.Add(account);
        // помощник по созданию
        app.Registration.Register(account);
    }

    [TearDown]
    //[TestFixtureTearDown]
    public void RestoreConfig()
    {
        //восстанавливаем файл припрятанный в начале
        app.Ftp.RestoreBackupFile("/config_inc.php");
    }*/
}