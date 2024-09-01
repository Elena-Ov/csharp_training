using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace AddressbookTestsWhite
{
    public class HelperBase
    {
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
        }
        
        // возможно нужно убрать
        public void Wait(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
        }
    }
}
