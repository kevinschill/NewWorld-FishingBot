using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NewFarm2
{
    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static IntPtr GetWindowRectA(IntPtr hWnd, ref Rect rect)
        {
            return GetWindowRect(hWnd, ref rect);
        }
        public static Bitmap CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }
        public static bool IsForegroundFullScreen(IntPtr hWnd)
        {
            return IsForegroundFullScreen(null, hWnd);
        }


        public static bool IsForegroundFullScreen(System.Windows.Forms.Screen screen, IntPtr hWnd)
        {

            if (screen == null)
            {
                screen = System.Windows.Forms.Screen.PrimaryScreen;
            }
            Rect rect = new Rect();

            GetWindowRect(hWnd, ref rect);

            if (screen.Bounds.Width == (rect.Right - rect.Left) && screen.Bounds.Height == (rect.Bottom - rect.Top))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }
        public static Bitmap CaptureWindow(IntPtr handle,int wideptr,int w2)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left+wideptr, rect.Top, (rect.Right - rect.Left) - w2, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }
        public static Bitmap CaptureWindow(IntPtr handle, int w, int h,bool ok)
        {
            var rect = new Rect();

            GetWindowRect(handle, ref rect);

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            var bounds = new Rectangle();

            if (IsForegroundFullScreen(handle))
            {
                bounds = new Rectangle(rect.Left + width / 4, rect.Top, (rect.Right - rect.Left) - Convert.ToInt32(w * 3.5), rect.Bottom - rect.Top);
            }
            else
            {
                bounds = new Rectangle(rect.Left + w, rect.Top, rect.Right - rect.Left - w * 2, rect.Bottom - rect.Top - h);
            }

            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }
    }
}
