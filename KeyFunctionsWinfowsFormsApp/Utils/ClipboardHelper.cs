using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFunctionsWinfowsFormsApp.Utils
{
    public class ClipboardHelper
    {
        [STAThread]
        public static bool TryGetText(out string clipboardText)
        {
            //if (Clipboard.ContainsImage())
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);
                return true;
            }
            clipboardText = null;
            return false;
        }
    }
}
