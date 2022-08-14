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

        private readonly ClipboardListenerService _clipboardListenerService;

        private IKeyboardMouseEvents m_GlobalHook;

        public Form1()
        {
            InitializeComponent();

            _clipboardListenerService = new ClipboardListenerService(this);
        }

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            //m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            //m_GlobalHook.MouseMoveExt += GlobalHookMouseDownExt;
            //m_GlobalHook.KeyPress += GlobalHookKeyPress;
            m_GlobalHook.KeyUp += GlobalHookKeyPress;
        }

        //private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        private void GlobalHookKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.Equals(Keys.C))
            {
                //Console.WriteLine("KeyValue:{0}, KeyData:{1}, KeyCode:{2}, Control:{3}, Alt:{4}, Shift:{5}", e.KeyValue, e.KeyData, e.KeyCode, e.Control, e.Alt, e.Shift);
            }
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            //Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);
            //Console.WriteLine("X: {0}; Y: {1}; System Timestamp: {2}", e.X, e.Y, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyUp -= GlobalHookKeyPress;

            // It is recommened to dispose it
            m_GlobalHook.Dispose();
        }


        protected override void WndProc(ref Message message)
        {
            _clipboardListenerService.WndProc(ref message, base.WndProc);
        }

        private void Form1_OnLoad(object sender, EventArgs e)
        {
            //User32.SetCursorPos(0, 0); // working

            //_clipboardListenerService.Subscribe();

            //Task.Run(() => PostInit());
            //Console.WriteLine(AppSettings.MyKeyInt);
            //Console.WriteLine(AppSettings.MyKeyBool);
            //Subscribe();
        }

        private void Form1_OnClosing(object sender, FormClosingEventArgs e)
        {
            //Unsubscribe();
        }
    }
}