using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FDP
{
    class SuspendResumeLayout
    {
        private const int WM_SETREDRAW = 0x000B;
        private const int WM_USER = 0x400;
        private const int EM_GETEVENTMASK = (WM_USER + 59);
        private const int EM_SETEVENTMASK = (WM_USER + 69);
        /*
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        //private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
        */

        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        public static IntPtr eventMask = IntPtr.Zero;
        public static void SuspendDrawing(Control parent)
        {
              SendMessage(parent.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
              eventMask = SendMessage(parent.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
        }

        public static void ResumeDrawing(Control parent)
        {
          SendMessage(parent.Handle, EM_SETEVENTMASK, 0, eventMask);
          SendMessage(parent.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
        }
    }
}
