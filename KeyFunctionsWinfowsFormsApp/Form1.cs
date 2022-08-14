using Gma.System.MouseKeyHook;
using KeyFunctionsWinfowsFormsApp.DLLDeclarations;
using KeyFunctionsWinfowsFormsApp.Services;
using KeyFunctionsWinfowsFormsApp.Utils;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace KeyFunctionsWinfowsFormsApp
{
    public partial class Form1 : Form
    {
        private static readonly Regex s_regexMis = new Regex("mis .*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Note: for the application hook, use the Hook.AppEvents() instead
        private readonly IKeyboardMouseEvents _GlobalHook = Hook.GlobalEvents();
        private readonly ClipboardListenerService _clipboardListenerService;

        public Form1()
        {
            InitializeComponent();

            _clipboardListenerService = new ClipboardListenerService(this);
        }

        private void Form1_OnLoad(object sender, EventArgs e)
        {
            //_clipboardListenerService.Subscribe();

            //Subscribe();

            //_ = User32.SetCursorPos(0, 0); // working
        }

        private void Form1_OnClosing(object sender, FormClosingEventArgs e)
        {
            //Unsubscribe();

            // It is recommened to dispose it
            _GlobalHook.Dispose();
        }

        protected override void WndProc(ref Message message)
        {
            _clipboardListenerService.WndProc(ref message, base.WndProc);
        }

        public void Subscribe()
        {
            _GlobalHook.MouseMoveExt += GlobalHook_MouseMoveExt;
            _GlobalHook.KeyUp += GlobalHook_KeyUp;
        }

        private void GlobalHook_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode.Equals(Keys.C))
            {
                //Console.WriteLine("KeyValue:{0}, KeyData:{1}, KeyCode:{2}, Control:{3}, Alt:{4}, Shift:{5}", e.KeyValue, e.KeyData, e.KeyCode, e.Control, e.Alt, e.Shift);
            }
        }

        private void GlobalHook_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            //Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);
            //Console.WriteLine("X: {0}; Y: {1}; System Timestamp: {2}", e.X, e.Y, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            _GlobalHook.MouseDownExt -= GlobalHook_MouseMoveExt;
            _GlobalHook.KeyUp -= GlobalHook_KeyUp;
        }
    }
}