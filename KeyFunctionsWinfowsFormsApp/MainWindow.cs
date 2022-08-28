using Gma.System.MouseKeyHook;
using KeyFunctions.Repository;
using KeyFunctions.Repository.Repositories;
using KeyFunctionsWinfowsFormsApp.DLLDeclarations;
using KeyFunctionsWinfowsFormsApp.Services;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace KeyFunctionsWinfowsFormsApp
{
    public partial class MainWindow : Form
    {
        private readonly ClipboardListenerService _clipboardListenerService;
        private readonly KeyboardListenerService _keyboardListenerService;

        public MainWindow()
        {
            InitializeComponent();

            _keyboardListenerService = new KeyboardListenerService();
            _clipboardListenerService = new ClipboardListenerService(this);
        }

        private void MainWindow_OnLoad(object sender, EventArgs e)
        {
            ClipboardService.CreateImagesDirectory();

            //ClipboardService.CopyImageFromDibBytesFile();
            //ClipboardService.CopyImageFromDibFalseBytesFile();
        }

        private void MainWindow_OnClosing(object sender, FormClosingEventArgs e)
        {
            _keyboardListenerService.UnSubscribe();
            _keyboardListenerService.Dispose();
        }

        protected override void WndProc(ref Message message)
        {
            _clipboardListenerService.WndProc(ref message, base.WndProc);
        }

        private void CheckBox_SetCursorPosition_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisbleKeyboardListener();
        }
        private void CheckBox_MaintainClipHistory_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisbleClipboardListener();
        }
        private void CheckBox_CleanSpecialCharacters_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisbleClipboardListener();
        }

        #region BUSINESS METHODS
        private void EnableOrDisbleKeyboardListener()
        {
            if (checkBoxSetCursorPosition.Checked)
            {
                _keyboardListenerService.Subscribe();
            }
            else
            {
                _keyboardListenerService.UnSubscribe();
            }
        }
        private void EnableOrDisbleClipboardListener()
        {
            if (checkBoxMaintainClipHistory.Checked || checkBoxCleanSpecialCharacters.Checked)
            {
                if (!_clipboardListenerService.IsSubscribed)
                {
                    _clipboardListenerService.Subscribe();
                }

                _clipboardListenerService.MaintainClipHistory = checkBoxMaintainClipHistory.Checked;
                _clipboardListenerService.CleanSpecialCharactersFromClip = checkBoxCleanSpecialCharacters.Checked;
            }
            else
            {
                _clipboardListenerService.Unsubscribe();
            }
        }
        #endregion
    }
}