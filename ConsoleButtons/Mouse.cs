using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleButtons
{
    public struct Mouse
    {
        public static AABB LocalMousePoint = new AABB(0, 0, 2, 2);
        public static MouseExt.POINT MousePoint = new MouseExt.POINT(0, 0);

        public static int ClickState = 0;
        public static bool Clicked = false;

        public void Update(Window.Rect WindowRect)
        {
            //Get global mouse position using PINVOKE
            MousePoint = MouseExt.GetCursorPosition();

            //Get mouse clicking state of the left button
            ClickState = MouseExt.GetAsyncKeyState(VK.LBUTTON);

            //Resets the button Clicked bool if not holding left button anymore
            if (ClickState == 0)
                Clicked = false;

            //Gets the mouse local window position (Otherwise it would count your whole desktop.
            LocalMousePoint.x = MousePoint.X - WindowRect.Left;
            LocalMousePoint.y = MousePoint.Y - WindowRect.Top;


            //Clamps the mouse position to the window.
            if (LocalMousePoint.x > WindowRect.Right - WindowRect.Left)
                LocalMousePoint.x = WindowRect.Right - WindowRect.Left;
            if (LocalMousePoint.x < 0)
                LocalMousePoint.x = 0;

            if (LocalMousePoint.y > WindowRect.Bottom - WindowRect.Top)
                LocalMousePoint.y = WindowRect.Bottom - WindowRect.Top;
            if (LocalMousePoint.y < 0)
                LocalMousePoint.y = 0;
        }
        public bool IsClicking()
        {
            return Convert.ToBoolean(ClickState & MouseExt.KeyPressed) && !Clicked;
        }
    }
}
