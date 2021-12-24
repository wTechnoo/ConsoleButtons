using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleButtons
{
    public class UIManager
    {
        private Process ThisProcess = Process.GetCurrentProcess();
        private IntPtr WindowHandlePtr;
        public static Window.Rect WindowRect;

        public List<UIComponent> Components;
        
        private Mouse Mouse;
        private int currentTop = 0;

        public UIManager()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            DisableConsoleQuickEdit.Go();

            WindowHandlePtr = ThisProcess.MainWindowHandle;
            WindowRect = new Window.Rect();
            Mouse = new Mouse();
            Components = new List<UIComponent>();
        }

        public void AddToComponents(UIComponent component)
        {
            Components.Add(component);
        }
        
        public void Update()
        {
            Window.GetWindowRect(WindowHandlePtr, ref WindowRect);
            Mouse.Update(WindowRect);

            for (int i = 0; i < Components.Count; i++)
            {
                UICollision(Components[i], Mouse.LocalMousePoint);
                Components[i].Update();
            }
        }
        
        public void UICollision(UIComponent component, AABB b)
        {
            AABB a = component.AABB;
            if (a.x < b.x + b.width &&
                a.x + a.width > b.x &&
                a.y < b.y + b.height &&
                a.y + a.height > b.y)
            {
                if (Mouse.Holding)
                {
                    component.Hold();
                }
                else if (Mouse.Clicked)
                {
                    component.Clicked();
                }
                else
                {
                    component.HoveringOver();
                }
            }
            else
            {
                component.StoppedHovering();
            }
        }

        public void Clear()
        {
            Console.Clear();
            currentTop = 0;
        }

        public void WriteLine(string text, ConsoleColor color)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop+currentTop);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            currentTop++;
        }
    }
    
    /*public interface IConsole<T>
    {
        T DefaultColor { get; set; }
        void WriteLine(string text, T Color);
        void Write(string text, T Color);
    }
    
    public class DefaultConsole : IConsole<ConsoleColor>
    {
        private ConsoleColor _defaultColor;
        public ConsoleColor DefaultColor
        {
            get { return _defaultColor; }
            set { _defaultColor = value; }
        }
        public void WriteLine(string text, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(text);
        }

        public void Write(string text, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.Write(text);
        }
    }*/
}
