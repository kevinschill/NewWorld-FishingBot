using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace NewFarm2
{
    internal class Funktions
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public static IntPtr FindWindowA(string windowName)
        {
            var hWnd = FindWindow(null, windowName);
            return hWnd;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

        public enum KeyCodes
        {
            TAB = 0x09,
            R = 0x52,
            E = 0x45,
            F3 = 0x72,
            W = 0x57,
            S = 0x53,
            A = 0x41,
            D = 0x44
        }
        public static void pressKey(KeyCodes k, int duration = 80)
        {
            keybd_event((uint)k, 0, 0x0001, 0);
            Thread.Sleep(duration);
            keybd_event((uint)k, 0, 0x0002, 0);
        }
        public static void holdKey(KeyCodes k)
        {
            keybd_event((uint)k, 0, 0x0001, 0);
        }
        public static void upKey(KeyCodes k)
        {
            keybd_event((uint)k, 0, 0x0002, 0);
        }





        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        public static void LeftMouseClick(Point pos)
        {

            mouse_event(MOUSEEVENTF_LEFTDOWN, pos.X, pos.Y, 0, 0);
            Thread.Sleep(50);
            mouse_event(MOUSEEVENTF_LEFTUP, pos.X, pos.Y, 0, 0);
        }
        public static void LeftMouseUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            //Thread.Sleep(rnd.Next(400, 1000));
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        public static void LeftMouseDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //Thread.Sleep(rnd.Next(400, 1000));
            //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
