using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleButtons
{
    public struct Mouse
    {
        public static Point ConsoleMousePoint = new Point(0, 0);
        public static AABB LocalMousePoint = new AABB(0, 0, 2, 2);
        public static Point MousePoint = new Point(0, 0);

        public static int PreviousClickState = 0;
        public static int ClickState = 0;
        public static bool Holding = false;
        public static bool Clicked = false;

        public void Update(Window.Rect windowRect)
        {
            ClickState = MouseExt.GetAsyncKeyState(VK.LBUTTON);
            MousePoint = MouseExt.GetCursorPosition();

            Holding = IsHolding();
            Clicked = IsPreviousHolding() && !IsHolding();

            LocalMousePoint.x = MousePoint.X - windowRect.Left;
            LocalMousePoint.y = MousePoint.Y - windowRect.Top;

            (ConsoleMousePoint.X, ConsoleMousePoint.Y) = Window.ConvertPxToConsole(LocalMousePoint.x, LocalMousePoint.y, windowRect);

            if (LocalMousePoint.x > windowRect.Right - windowRect.Left)
                LocalMousePoint.x = windowRect.Right - windowRect.Left;
            if (LocalMousePoint.x < 0)
                LocalMousePoint.x = 0;

            if (LocalMousePoint.y > windowRect.Bottom - windowRect.Top)
                LocalMousePoint.y = windowRect.Bottom - windowRect.Top;
            if (LocalMousePoint.y < 0)
                LocalMousePoint.y = 0;

            PreviousClickState = ClickState;
        }
        public bool IsPreviousHolding()
        {
            return Convert.ToBoolean(PreviousClickState & MouseExt.MousePressed);
        }
        public bool IsHolding()
        {
            return Convert.ToBoolean(ClickState & MouseExt.MousePressed);
        }
    }
}