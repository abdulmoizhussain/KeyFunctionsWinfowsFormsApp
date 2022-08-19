using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFunctions.Common.Utils
{
    public class AppSettings
    {
        //public static bool MyKeyBool => bool.Parse(ConfigurationManager.AppSettings["MyKeyBool"]);
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
    }
}
