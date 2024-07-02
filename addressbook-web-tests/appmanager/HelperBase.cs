using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests;

    public class HelperBase
    {
        protected IWebDriver driver;
        protected ApplicationManager manager;
        

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver; // на вход принимает ссылку на driver который управляет браузером и присваивает ее в поле
        }
        public void Type(By locator, string text)// возможность менять поля по отдельности
        {
            if (text != null) // если значение в поле у нас меняется !=null, даже если на пустое, то заполняем новыми данными, иначе не делаем ничего
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }
    }