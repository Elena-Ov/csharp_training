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
    }