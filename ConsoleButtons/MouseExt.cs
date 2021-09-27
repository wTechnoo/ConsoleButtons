using System;
using System.Runtime.InteropServices;

namespace ConsoleButtons
{
    public static class MouseExt
    {
        public static int KeyPressed = 0x8000;
        [DllImport("user32.dll")]
        public static extern short GetKeyState(VK nVirtKey);

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(VK vKeys);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        public static POINT GetCursorPosition()
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

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

            public POINT(POINT p)
            {
                X = p.X;
                Y = p.Y;
            }
        }
    }
}
