using NUnit.Framework;
using System.IO;

namespace mantis_tests;

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
   [Test]
    public void TestAccountRegistration()
    {
        AccountData account = new AccountData()
        {
            UserName = "testuser",
            Password = "password",
            Email = "testuser@localhost.localdomain"
        };
        List<AccountData> accounts = app.Admin.GetAllAccounts();
        //в качестве параметра передаем лямбда выражение, которое вернет True или False
        AccountData existingAccount = accounts.Find(x => x.UserName == account.UserName);
        
        if (existingAccount != null)
        {
            app.Admin.DeleteAccount(existingAccount);
        }
        
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
    }
}