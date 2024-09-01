using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace AddressbookTestsAutoit
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private GroupHelper groupHelper;
        private AutoItX3 aux;
        public ApplicationManager()
        {
            aux = new AutoItX3();
            // !!! прописать путь к приложению
            aux.Run(@"путь к десктопному приложению", "", aux.SW_SHOW);
            aux.WinWait(WINTITLE);
            aux.WinActivate(WINTITLE);
            aux.WinWaitActive(WINTITLE);
            
            groupHelper = new GroupHelper(this);
        }
        public void Stop()
        {
            // параметры - название окна в котором нажимаем кнопку, текст кнопки(необязат), идентификатор кнопки (локатор)
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.62e44910");

        }

        public AutoItX3 Aux
        {
            get
            {
                return aux;
            }
        }

        public GroupHelper Groups
        {
            get { return groupHelper; }
        }
    }
}

