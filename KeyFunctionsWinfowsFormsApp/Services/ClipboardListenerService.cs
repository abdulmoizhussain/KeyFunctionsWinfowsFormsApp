using KeyFunctions.Common.Enums;
using KeyFunctions.Repository.Repositories;
using KeyFunctionsWinfowsFormsApp.DLLDeclarations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private static readonly Regex s_regexMis = new Regex("^mis .*", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private static readonly Regex s_regexMisReplacer = new Regex("^mis ", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private static readonly Regex s_regexCleaner = new Regex("[^a-zA-Z0-9 ]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly Form _form;

        private IntPtr _clipboardViewerNext;
        private DateTime _subscriptionTime;

        public bool MaintainClipHistory { get; set; }
        public bool CleanSpecialCharactersFromClip { get; set; }
        public bool IsSubscribed { get; private set; }

        public ClipboardListenerService(Form form)
        {
            _form = form;
        }

        public void Subscribe()
        {
            _subscriptionTime = DateTime.Now;
            //_clipboardViewerNext = User32.SetClipboardViewer(this.Handle);
            _clipboardViewerNext = User32.SetClipboardViewer(_form.Handle);
            IsSubscribed = true;
        }

        public void Unsubscribe()
        {
            //User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
            User32.ChangeClipboardChain(_form.Handle, _clipboardViewerNext);
            IsSubscribed = false;
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

                    // Here you will be able to grab clipboard data.
                    // whenever we subscribe to the clipboard listener, it automatically takes the current clipboard data which was copied before subscription and provides us as first update after subscription, which we do not want in this app.
                    // This why, have put this if-check below.
                    if ((DateTime.Now - _subscriptionTime).TotalMilliseconds > 1500)
                    {
                        PerformClipboardManipulation(message);
                    }

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

        private void PerformClipboardManipulation(Message _)
        {
            ClipDataType clipDataType = ClipboardService.GetDataType();

            if (clipDataType.Equals(ClipDataType.Text))
            {
                string clipText = ClipboardService.GetText();
                Console.WriteLine($"clip: {clipText}");
                TryPreserveClipboardText(clipText);
                TryCleanSpecialCharacterFromClipboard(clipText);
            }
            else if (clipDataType.Equals(ClipDataType.Image))
            {
                TryPreserveClipboardImage();
            }
        }

        private void TryPreserveClipboardText(string clipText)
        {
            if (MaintainClipHistory)
            {
                Task.Run(() => ClipboardHistoryRepository.AddOrUpdateOne(clipText, ClipDataType.Text));
            }
        }

        private void TryPreserveClipboardImage()
        {
            if (MaintainClipHistory)
            {
                //ClipboardService.SaveImageAsDibBytes();
                //ClipboardService.TrySaveClipboardImageAndGetFileName(out string filename);
                //ClipboardService.GetImageFromClipboard();
                //ClipboardService.TrySaveRawImageData();
                //Task.Run(() => ClipboardHistoryRepository.AddOrUpdateOne(filename, ClipDataType.Image));
            }
        }

        private void TryCleanSpecialCharacterFromClipboard(string clipText)
        {
            if (CleanSpecialCharactersFromClip && s_regexMis.IsMatch(clipText))
            {
                // clean the clipboard text
                string clipTextCleaned = s_regexCleaner.Replace(clipText, " ");
                string cleanedText = s_regexMisReplacer.Replace(clipTextCleaned, "").Trim();

                ClipboardService.SetText(cleanedText);
            }
        }
    }
}
