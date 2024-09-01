using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookTestsAutoit

{
    public class TestBase
    {
         public ApplicationManager app;

         [TestFixtureSetUp]
         public void InitApplication()
         {
             app = new ApplicationManager();
         }

         [TestFixtureTearDown]
         public void StopApplication()
         {
             app.Stop();
         }
    } 
}
