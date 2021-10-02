using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleButtons
{
    public class ConsoleClick
    {
        private Process ThisProcess = Process.GetCurrentProcess();
        private IntPtr WindowHandlePtr;
        public static Window.Rect WindowRect;

        public List<UIComponent> Components;
        private Mouse Mouse;

        public ConsoleClick()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            DisableConsoleQuickEdit.Go();

            WindowHandlePtr = ThisProcess.MainWindowHandle;
            WindowRect = new Window.Rect();
            Mouse = new Mouse();
            Components = new List<UIComponent>();
        }
        public void Initialize()
        {
            Window.GetWindowRect(WindowHandlePtr, ref WindowRect);
            Mouse.Update(WindowRect);
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
                CollisionDetection(Components[i], Mouse.LocalMousePoint);
                Components[i].Update();
            }
        }
        public void CollisionDetection(UIComponent component, AABB b)
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
                    component.HoveringOver();
            }
            else
            {
                component.StoppedHovering();
            }
        }
    }
}
