namespace MantisTests;

public class AuthTestBase : TestBase
{
    [SetUp]
    // второй уровень, базовый класс для всех тестов которые требуют вход в систему
    public void SetUpLogin()
    {
        app.Auth.Login(new AccountData("administrator", "root"));
    }
}