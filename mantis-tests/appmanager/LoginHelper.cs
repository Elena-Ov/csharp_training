using OpenQA.Selenium;

namespace mantis_tests;

public class LoginHelper : HelperBase
{
    private string baseURL;
    public LoginHelper(ApplicationManager manager, string baseUrl) : base(manager) 
    {baseURL = baseUrl;} // вызов конструктора базового класса
    public void Login(AccountData account) // параметр типа AccountData
    {
        if (IsLoggedIn()) // проверяем залогинены или нет
        {
            return;
        }
        // если проверили и выяснилось что не залогинены, то выполняется код для входа в систему
        driver.Url = baseURL + "login_page.php";
        Type(By.Name("username"), account.UserName);
        driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
        Type(By.Name("password"), account.Password);
        driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
    }
    public bool IsLoggedIn() // проверка находимся ли мы внутри сессии, вошли в приложение
    {
        return IsElementPresent(By.ClassName("login-form"));
    }
}