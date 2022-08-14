using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFunctionsWinfowsFormsApp.Utils
{
    internal class AppSettings
    {
        public static bool MyKeyBool => bool.Parse(ConfigurationManager.AppSettings["MyKeyBool"]);
        public static int MyKeyInt => int.Parse(ConfigurationManager.AppSettings["MyKeyInt"]);
    }
}
