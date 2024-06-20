using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests;

public class TestBase
{   
    protected ApplicationManager app;
    
    [SetUp]
    public void SetupTest() //методы для инициализации
    {   
        app = new ApplicationManager(); // ссылка, инициализируем ApplicationManager в методе
    }
        
    [TearDown]
    public void TeardownTest() // метод который останавливает драйвер в конце
    {
        app.Stop();
    }
}
