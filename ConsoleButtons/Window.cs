using System;
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

        public static int Lerp(int a, int b, int t)
        {
            return a * (1 - t) + b * t;
        }
        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }
        public static float Normalize(float val, float valmin, float min, float max)
        {
            return (val - valmin) / (max - min);
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
}