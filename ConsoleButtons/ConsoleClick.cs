using System;
using System.Collections.Generic;
using System.Diagnostics;
using Console = Colorful.Console;

namespace ConsoleButtons
{
    public class ConsoleClick
    {
        private Process ThisProcess = Process.GetCurrentProcess();
        private IntPtr WindowHandlePtr;
        public Window.Rect WindowRect;

        public List<UIComponent> Components;
        public Mouse Mouse;

        public ConsoleClick()
        {
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
                CollisionDetection(Components[i], Mouse.LocalMousePoint);
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
