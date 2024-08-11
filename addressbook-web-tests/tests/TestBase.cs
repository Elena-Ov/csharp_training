using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Runtime;
using NUnit.Framework.Constraints;

namespace WebAddressbookTests;

public class TestBase
{
    // если нужно отключить проверку -> PERFORM_LONG_UI_CHECKS = false;
    public static bool PERFORM_LONG_UI_CHECKS = true;
    protected ApplicationManager app;

    [SetUp]
    public void SetupApplicationManager() // самый верхний уровень в котором инициализируется ApplicationManager
    {
        app = ApplicationManager.GetInstance();
    }

    // метод генерации данных для групп и контактов
    
    // создаем генератор случайных чисел
    public static Random rnd = new Random();
    public static string GenerateRandomString(int max)
    {
        // создаем случайное число в диапазоне от 0 до указ max
        // NextDouble() сгенерирует число в диапозоне от 0 до 1, мы умножим его на max
        // полученное значение округлим, преобразуем из дробного в целое Convert.ToInt32()
        int l = Convert.ToInt32(rnd.NextDouble() * max);
        // генерируем случайные символы и формируем из них строку
        StringBuilder builder = new StringBuilder();
        // генерируем l различных символов
        for (int i = 0; i < l; i++)
        {
            // коды символов в соответсвиии с таблицей ASCII
            // полученное число конвертируем в целое число, а его в символ
            builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
        }

        // извлекаем из билдера полученную строку
        return builder.ToString();
    }
}

