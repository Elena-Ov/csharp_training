using OpenQA.Selenium;

namespace MantisTests;

public class LoginHelper : HelperBase
{
    private string baseURL;
    public LoginHelper(ApplicationManager manager) : base(manager) {} // вызов конструктора базового класса
    public void Login(AccountData account) // параметр типа AccountData
    {
        if (IsLoggedIn()) // проверяем залогинены или нет
        {
            return;
        }
        // если проверили и выяснилось что не залогинены, то выполняется код для входа в систему
        driver.Url = baseURL + "login_page.php";
        Type(By.Name("username"), account.Name);
        driver.FindElement(By.CssSelector("input.button")).Click();
        Type(By.Name("password"), account.Password);
        driver.FindElement(By.CssSelector("input.button")).Click();
    }
    public bool IsLoggedIn() // проверка находимся ли мы внутри сессии, вошли в приложение
    {
        return IsElementPresent(By.ClassName("span.user-info"));
    }
}