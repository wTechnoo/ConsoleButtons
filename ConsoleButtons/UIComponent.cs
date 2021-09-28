using System;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleButtons
{
    public class UIComponent
    {
        public AABB AABB;
        public Point ConsolePosition;
        public bool IsHoveringOver;

        protected bool initialized;

        public event Action OnClick;
        public event Action OnHold;
        public event Action OnHoverOver;
        public event Action OnHoverStop;

        public virtual void Clicked() => OnClick?.Invoke();
        public virtual void Hold() => OnHold?.Invoke();
        public virtual void HoveringOver()
        {
            OnHoverOver?.Invoke();
            IsHoveringOver = true;
        }
        public void StoppedHovering()
        {
            OnHoverStop?.Invoke();
            IsHoveringOver = false;
        }
    }

    public class AABB
    {
        public int x, y;
        public int width, height;

        public AABB(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            width = w;
            height = h;
        }
    }

    public class Button : UIComponent
    {
        private string text;

        public Button(string text, int x, int y, int w, int h)
        {
            this.text = text;
            AABB = new AABB(x, y, w, h);
            ConsolePosition = new Point(x, y);
        }
        public Button(string text, int x, int y, int w, int h, Window.Rect WindowRect)
        {
            this.text = text;
            AABB = new AABB(x, y, w, h);
            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init(WindowRect);
        }
        public Button(string text, int x, int y, Window.Rect WindowRect)
        {
            this.text = text;
            AABB = new AABB(x, y, text.Length * 9, 16);
            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init(WindowRect);
        }

        public void Init(Window.Rect WindowRect)
        {
            if (initialized)
                return;

            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(text);

            (AABB.x, AABB.y) = Window.ConvertConsoleToPx(AABB.x, AABB.y, WindowRect);

            initialized = true;
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
    public class CheckBox : UIComponent
    {
        public bool IsChecked;

        private string text;
        private char markChar;

        public CheckBox(string text, char markChar, bool isChecked, int x, int y, Window.Rect WindowRect)
        {
            this.text = text;
            this.markChar = markChar;
            IsChecked = isChecked;

            AABB = new AABB(x + 1, y, 20, 16);
            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init(WindowRect);
        }
        public CheckBox(string text, char markChar, bool isChecked, bool collideWithText, int x, int y, Window.Rect WindowRect)
        {
            this.text = text;
            this.markChar = markChar;
            IsChecked = isChecked;

            AABB = new AABB(x + 1, y, collideWithText ? 20 + ("  " + text).Length * 8 : 20, 16);

            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init(WindowRect);
        }

        public void Init(Window.Rect WindowRect)
        {
            if (initialized)
                return;

            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(text);

            (AABB.x, AABB.y) = Window.ConvertConsoleToPx(AABB.x, AABB.y, WindowRect);

            initialized = true;
        }

        public override void Clicked()
        {
            base.Clicked();
            IsChecked = !IsChecked;
        }

        public void WriteWithNoColor()
        {
            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(IsChecked ? $"[{markChar}] {text}" : $"[ ] {text}", Color.White);
        }
        public void WriteWithColor(Color color)
        {
            Console.SetCursorPosition(ConsolePosition.X, ConsolePosition.Y);
            Console.WriteLine(IsChecked ? $"[{markChar}] {text}" : $"[ ] {text}", color);
        }
    }
}