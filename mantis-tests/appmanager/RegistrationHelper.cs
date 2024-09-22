using OpenQA.Selenium;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace MantisTests;

public class RegistrationHelper : HelperBase
{
    //конструктор, который обращается к конструктору базового класса
    //в качестве параметра принимает ApplicationManager и передает в конструктор базового класса
    public RegistrationHelper(ApplicationManager manager) : base(manager){}

    public void Register(AccountData account)
    {
        OpenMainPage();
        OpenRegistrationForm();
        FillRegistrationForm(account);
        SubmitRegistration();
        String url = GetConfirmationUrl(account);
        FillPasswordForm(url, account);
        SubmitPasswordForm();
    }

    public string GetConfirmationUrl(AccountData account)
    {
        String message = manager.Mail.GetLastMail(account);
        //для извлечения ссылки используем регулярное выражение
        Match match = Regex.Match(message, @"http://\S*");
        return match.Value;
    }

    public void FillPasswordForm(string url, AccountData account)
    {
        driver.Url = url;
        driver.FindElement(By.Name("password")).SendKeys(account.Password);
        driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
    }

    public void SubmitPasswordForm()
    {
        driver.FindElement(By.CssSelector("input.button")).Click();
    }

    public void OpenMainPage()
    {
        manager.Driver.Url = "http://localhost/mantisbt-2.26.3/login_page.php";
    }

    public void OpenRegistrationForm()
    {
        driver.FindElements(By.CssSelector("span.bracket-link"))[0].Click();
    }

    public void FillRegistrationForm(AccountData account)
    {
        driver.FindElement(By.Name("username")).SendKeys(account.UserName);
        driver.FindElement(By.Name("email")).SendKeys(account.Email);
    }

    public void SubmitRegistration()
    {
        driver.FindElement(By.CssSelector("input.button")).Click();
    }
}