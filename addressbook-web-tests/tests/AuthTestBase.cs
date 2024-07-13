using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

public class AuthTestBase : TestBase// второй уровень, базовый класс для всех тестов которые требуют вход в систему
{
    [SetUp]
    public void SetupLogin() //методы для инициализации
    {   
        //app = ApplicationManager.GetInstance(); удалить
        app.Auth.Login(new AccountData("admin", "secret"));
    } //app.Auth.LogOut();
}