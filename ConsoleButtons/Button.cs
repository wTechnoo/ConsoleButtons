using System;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleButtons
{
    public class Button
    {
        //AABB Definition
        public AABB AABB;

        //Button Variables
        public MouseExt.POINT ConsolePosition;
        public bool IsHoveringOver;
        private string text;
        private bool initialized = false;

        //Button events
        public event Action OnClick;
        public event Action OnHoverOver;
        public event Action OnHoverStop;

        //Constructors
        //Define the width and height of button collider manually.
        public Button(string text, int x, int y, int w, int h)
        {
            this.text = text;
            AABB = new AABB(x, y, w, h);
            ConsolePosition = new MouseExt.POINT(x, y);
        }

        //Gets width and height of button automatically based on how many characters there are.
        public Button(string text, int x, int y, int w, int h, Window.Rect WindowRect)
        {
            this.text = text;
            AABB = new AABB(x, y, w, h);
            ConsolePosition = new MouseExt.POINT(x, y);

            if (!initialized)
                Init(WindowRect);
        }
        public Button(string text, int x, int y, Window.Rect WindowRect)
        {
            this.text = text;
            AABB = new AABB(x, y, text.Length * 8, 16);
            ConsolePosition = new MouseExt.POINT(x, y);

            if (!initialized)
                Init(WindowRect);
        }

        //Auto initalizes when using constructor that receives WindowRect
        public void Init(Window.Rect WindowRect)
        {
            if (initialized)
                return;

            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(text);

            //Convert console button position to "pixel" button position (NECESSARY FOR THE AUTOMATIC X AND Y CONVERSION).
            (AABB.x, AABB.y) = Window.ConvertConsoleToPx(AABB.x, AABB.y, WindowRect);

            initialized = true;
        }

        public void Clicked() => OnClick?.Invoke();
        public void HoveringOver()
        {
            OnHoverOver?.Invoke();
            IsHoveringOver = true;
        }
        public void StoppedHovering()
        {
            OnHoverStop?.Invoke();
            IsHoveringOver = false;
        }

        public void WriteWithNoColor()
        {
            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(text, Color.White);
        }
        public void WriteWithColor(Color color)
        {
            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(text, color);
        }
    }
}
