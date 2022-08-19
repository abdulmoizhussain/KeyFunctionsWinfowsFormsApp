using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyFunctionsWinfowsFormsApp.Services
{
    internal class KeyboardListenerService
    {
        // Note: for the application hook, use the Hook.AppEvents() instead
        private readonly IKeyboardMouseEvents _GlobalHook = Hook.GlobalEvents();

        // source links:
        // https://stackoverflow.com/a/12816899
        private static readonly int s_screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static readonly int s_screenHeight = Screen.PrimaryScreen.Bounds.Height;

        private readonly Point _point_MiddleTop = new(s_screenWidth / 2, 0);
        private readonly Point _point_TopRight = new(s_screenWidth, 0);
        private readonly Point _point_MiddleRight = new(s_screenWidth, s_screenHeight / 2);
        private readonly Point _point_BottomRight = new(s_screenWidth, s_screenHeight);
        private readonly Point _point_MiddleBottom = new(s_screenWidth / 2, s_screenHeight);
        private readonly Point _point_BottomLeft = new(0, s_screenHeight);
        private readonly Point _point_MiddleLeft = new(0, s_screenHeight / 2);
        private readonly Point _point_TopLeft = new(0, 0);

        public KeyboardListenerService()
        {
            //_ = User32.SetCursorPos(0, 0); // working
        }

        public void Subscribe()
        {
            _GlobalHook.MouseMoveExt += GlobalHook_MouseMoveExt;
            _GlobalHook.KeyUp += GlobalHook_KeyUp;
        }

        public void UnSubscribe()
        {
            _GlobalHook.MouseDownExt -= GlobalHook_MouseMoveExt;
            _GlobalHook.KeyUp -= GlobalHook_KeyUp;
        }

        public void Dispose()
        {
            // It is recommened to dispose it
            _GlobalHook.Dispose();
        }

        #region PRIVATE METHODS
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
        #endregion
    }
}
