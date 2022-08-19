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
    public partial class Form1 : Form
    {
        private readonly ClipboardListenerService _clipboardListenerService;
        private readonly KeyboardListenerService _keyboardListenerService;

        public Form1()
        {
            InitializeComponent();

            _keyboardListenerService = new KeyboardListenerService();
            _clipboardListenerService = new ClipboardListenerService(this);
        }

        private void Form1_OnLoad(object sender, EventArgs e)
        {
            ClipboardHistoryRepository.AddOne();
            ClipboardHistoryRepository.AddOne();

            var asdf = ClipboardHistoryRepository.GetAll();
        }

        private void Form1_OnClosing(object sender, FormClosingEventArgs e)
        {
            _keyboardListenerService.UnSubscribe();
            _keyboardListenerService.Dispose();
        }

        protected override void WndProc(ref Message message)
        {
            _clipboardListenerService.WndProc(ref message, base.WndProc);
        }

        private void checkBoxSetCursorPosition_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisbleKeyboardListener();
        }
        private void checkBoxMaintainClipHistory_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisbleClipboardListener();
        }
        private void checkBoxCleanSpecialCharacters_CheckedChanged(object sender, EventArgs e)
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