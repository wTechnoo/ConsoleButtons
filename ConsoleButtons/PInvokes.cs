using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleButtons
{
    public static class Window
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr FindWindow(string strClassName, string strWindowName);

            [DllImport("user32.dll")]
            public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

            public struct Rect
            {
                public int Left { get; set; }
                public int Top { get; set; }
                public int Right { get; set; }
                public int Bottom { get; set; }
            }

            public static int Remap(int value, int low1, int high1, int low2, int high2)
            {
                return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
            }
            public static float Remap(float value, float low1, float high1, float low2, float high2)
            {
                return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
            }

            public static (int x, int y) ConvertConsoleToPx(int X, int Y, Rect WindowRect)
            {
                return (Remap(X, 0, Console.BufferWidth, 0, WindowRect.Right - WindowRect.Left - X / 3), Remap(Y, 0, Console.WindowHeight, 30, WindowRect.Bottom - WindowRect.Top - Convert.ToInt32(Y / 1.3)));
            }

            //public static (int x, int y) ConvertPxToConsole(int X, int Y, Rect WindowRect)
            //{
            //    return (Remap(X, 0, WindowRect.Right - WindowRect.Left, 0, Console.WindowWidth), Remap(Y, 30, WindowRect.Bottom - WindowRect.Top, 0, Console.WindowHeight));
            //}

            public static (int x, int y) ConvertPxToConsole(int X, int Y, Rect WindowRect)
            {
                return (Remap(X, 0, WindowRect.Right - WindowRect.Left, 0, Console.WindowWidth + X / 200), Remap(Y, 30, WindowRect.Bottom - WindowRect.Top, 0, Console.WindowHeight + Y / 200));
            }
        }
    public static class MouseExt
    {
        public const int MousePressed = 0x8000;

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(VK vKeys);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

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
    }
    public enum VK : int
    {
        LBUTTON = 0x01,
        RBUTTON = 0x02,
        MBUTTON = 0x04
    }
    public static class DisableConsoleQuickEdit
    {
        const uint ENABLE_QUICK_EDIT = 0x0040;

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
        const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        internal static bool Go()
        {
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            uint consoleMode;
            if (!GetConsoleMode(consoleHandle, out consoleMode))
            {
                // ERROR: Unable to get console mode.
                return false;
            }

            // Clear the quick edit bit in the mode flags
            consoleMode &= ~ENABLE_QUICK_EDIT;

            // set the new mode
            if (!SetConsoleMode(consoleHandle, consoleMode))
            {
                // ERROR: Unable to set console mode
                return false;
            }

            return true;
        }
    }
}