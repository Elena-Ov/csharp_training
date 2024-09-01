using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace AddressbookTestsWhite
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private GroupHelper groupHelper;
        private ContactHelper contactHelper;
        // !!! прописать путь к исполняемому файлу десктопного приложения
        private static string ExePath = @"путь к исполняемому файлу";
        
        public ApplicationManager()
        {
            Application app = Application.Launch(ExePath);
            MainWindow = app.GetWindow(WINTITLE);
            
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }
        
        public Window MainWindow { get; private set; }
        public void Stop()
        {
            MainWindow.Get<Button>("uxExitAddressButton").Click();
        }
        public GroupHelper Groups { get { return groupHelper; } }
        public ContactHelper Contacts { get { return contactHelper; } }
    }
}

