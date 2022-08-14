using KeyFunctionsWinfowsFormsApp.DLLDeclarations;
using KeyFunctionsWinfowsFormsApp.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFunctionsWinfowsFormsApp.Services
{
    /// <summary>
    /// source links:
    /// https://stackoverflow.com/questions/621577/how-do-i-monitor-clipboard-changes-in-c
    /// https://web.archive.org/web/20131104125500/http://www.radsoftware.com.au/articles/clipboardmonitor.aspx
    /// </summary>
    public class ClipboardListenerService
    {
        public delegate void WndProcDelegate(ref Message message);

        private readonly Form _form;

        private IntPtr _clipboardViewerNext;

        public ClipboardListenerService(Form form)
        {
            _form = form;
        }

        public void Subscribe()
        {
            //_clipboardViewerNext = User32.SetClipboardViewer(this.Handle);
            _clipboardViewerNext = User32.SetClipboardViewer(_form.Handle);
        }

        public void Unsubscribe()
        {
            //User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
            User32.ChangeClipboardChain(_form.Handle, _clipboardViewerNext);
        }

        public void WndProc(ref Message message, WndProcDelegate wndProcBase)
        {
            switch ((Msgs)message.Msg)
            {
                //
                // The WM_DRAWCLIPBOARD message is sent to the first window 
                // in the clipboard viewer chain when the content of the 
                // clipboard changes. This enables a clipboard viewer 
                // window to display the new content of the clipboard.
                //
                case Msgs.WM_DRAWCLIPBOARD:
                    Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + message.Msg, "WndProc");

                    ClipboardHelper.TryGetText(out string clipText);
                    Console.WriteLine($"clip: {clipText}");

                    //
                    // Each window that receives the WM_DRAWCLIPBOARD message 
                    // must call the SendMessage function to pass the message 
                    // on to the next window in the clipboard viewer chain.
                    //
                    _ = User32.SendMessage(_clipboardViewerNext, message.Msg, message.WParam, message.LParam);
                    break;

                //
                // The WM_CHANGECBCHAIN message is sent to the first window 
                // in the clipboard viewer chain when a window is being 
                // removed from the chain. 
                //
                case Msgs.WM_CHANGECBCHAIN:
                    Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + message.LParam, "WndProc");

                    // When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
                    // it should call the SendMessage function to pass the message to the 
                    // next window in the chain, unless the next window is the window 
                    // being removed. In this case, the clipboard viewer should save 
                    // the handle specified by the lParam parameter as the next window in the chain. 

                    //
                    // wParam is the Handle to the window being removed from 
                    // the clipboard viewer chain 
                    // lParam is the Handle to the next window in the chain 
                    // following the window being removed. 
                    if (message.WParam == _clipboardViewerNext)
                    {
                        //
                        // If wParam is the next clipboard viewer then it
                        // is being removed so update pointer to the next
                        // window in the clipboard chain
                        //
                        _clipboardViewerNext = message.LParam;
                    }
                    else
                    {
                        _ = User32.SendMessage(_clipboardViewerNext, message.Msg, message.WParam, message.LParam);
                    }
                    break;

                default:
                    //
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    //base.WndProc(ref message);
                    wndProcBase(ref message);
                    break;
            }
        }
    }
}
