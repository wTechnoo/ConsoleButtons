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

        public virtual void Update() { }
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

            if (!initialized)
                Init();
        }
        public Button(string text, int x, int y)
        {
            this.text = text;
            AABB = new AABB(x, y, text.Length * 9, 16);
            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init();
        }

        public void Init()
        {
            if (initialized)
                return;

            WriteWithNoColor();

            (AABB.x, AABB.y) = Window.ConvertConsoleToPx(AABB.x, AABB.y, ConsoleClick.WindowRect);

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

        public CheckBox(string text, char markChar, bool isChecked, int x, int y)
        {
            this.text = text;
            this.markChar = markChar;
            IsChecked = isChecked;

            AABB = new AABB(x, y, 20, 16);
            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init();
        }
        public CheckBox(string text, char markChar, bool isChecked, bool collideWithText, int x, int y)
        {
            this.text = text;
            this.markChar = markChar;
            IsChecked = isChecked;

            AABB = new AABB(x, y, collideWithText ? 20 + ("  " + text).Length * 8 : 20, 16);

            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init();
        }

        public void Init()
        {
            if (initialized)
                return;

            WriteWithNoColor();

            (AABB.x, AABB.y) = Window.ConvertConsoleToPx(AABB.x, AABB.y, ConsoleClick.WindowRect);

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
    public class Slider : UIComponent
    {
        public float Value;

        private int size;
        //private float minValue, maxValue;
        private float maxValue;
        private bool toInt;
        private char fillChar, unfilledChar;

        public Slider(float initialValue, float maxValue, int size, bool toInt, char fillChar, char unfilledChar, int x, int y)
        {
            Value = initialValue;

            this.fillChar = fillChar;
            this.toInt = toInt;
            this.unfilledChar = unfilledChar;
            this.size = size;
            //this.minValue = minValue;
            this.maxValue = maxValue;

            AABB = new AABB(x+1, y, 8 * size, 16);
            ConsolePosition = new Point(x, y);

            if (!initialized)
                Init();
        }

        public override void Hold()
        {
            (int CX, int CY) = Window.ConvertConsoleToPx(ConsolePosition.X, ConsolePosition.Y, ConsoleClick.WindowRect);
            (int conSize, _) = Window.ConvertConsoleToPx(size, 0, ConsoleClick.WindowRect);

            float remappedValues = Window.Remap(Mouse.LocalMousePoint.x - CX / 600, CX + 7, CX + conSize, 0, maxValue);
            Value = toInt ? (int)remappedValues : remappedValues;

            base.Hold();
        }

        public void Init()
        {
            if (initialized)
                return;

            WriteWithNoColor();

            (AABB.x, AABB.y) = Window.ConvertConsoleToPx(AABB.x, AABB.y, ConsoleClick.WindowRect);

            initialized = true;
        }
        public void WriteWithNoColor()
        {
            Console.SetCursorPosition(ConsolePosition.X - 1, ConsolePosition.Y);
            Console.WriteLine('[', Color.White);

            Console.SetCursorPosition(ConsolePosition.X + size, ConsolePosition.Y);
            Console.WriteLine(']', Color.White);

            for (int i = 0; i < size; i++)
            {
                Console.SetCursorPosition(ConsolePosition.X + i, ConsolePosition.Y);
                float value = (Value / maxValue) * size;
                if (i < value)
                    Console.WriteLine(fillChar, Color.White);
                else
                    Console.WriteLine(unfilledChar, Color.White);
            }
            
        }
        public void WriteWithColor(Color color)
        {
            Console.SetCursorPosition(ConsolePosition.X - 1, ConsolePosition.Y);
            Console.WriteLine('[', color);

            Console.SetCursorPosition(ConsolePosition.X + size, ConsolePosition.Y);
            Console.WriteLine(']', color);

            for (int i = 0; i < size; i++)
            {
                Console.SetCursorPosition(ConsolePosition.X + i, ConsolePosition.Y);
                float value = (Value / maxValue) * size;
                if (i < value)
                    Console.WriteLine(fillChar, color);
                else
                    Console.WriteLine(unfilledChar, color);
            }
        }
        public override void Update()
        {
            //if (Value <= minValue)
            //    Value = minValue;
            if (Value <= 0)
                Value = 0;
            if (Value >= maxValue)
                Value = maxValue;
        }
    }
}