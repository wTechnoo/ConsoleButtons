using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleButtons
{
	public class UIManager
	{
		readonly List<UIComponent> _components;
		int _currentTop = 0;

		Mouse _mouse;
		Window.Rect _windowRect;

		public UIManager()
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.CursorVisible = false;
			DisableConsoleQuickEdit.Go();

			_windowRect = new Window.Rect();
			_mouse = new Mouse();
			_components = new List<UIComponent>();
		}

		public void RemoveComponent(UIComponent component)
		{
			_components.Remove(component);
		}

		public void AddToComponents(UIComponent component)
		{
			_components.Add(component);
		}

		public void Update()
		{
			Window.GetWindowRect(ConsoleMetrics.Handle, ref _windowRect);
			_mouse.Update(_windowRect);

			for (var i = 0; i < _components.Count; i++)
			{
				UICollision(_components[i], Mouse.ConsoleMousePoint.X, Mouse.ConsoleMousePoint.Y);
				_components[i].Update();
			}
		}

		public void UICollision(UIComponent component, int column, int row)
		{
			if (component.AABB.Contains(column, row))
			{
				if (Mouse.Holding)
				{
					component.Hold();
					component.IsHoveringOver = false;
				}
				else if (Mouse.Clicked)
				{
					component.Clicked();
					component.IsHoveringOver = false;
				}
				else if (!component.IsHoveringOver)
					component.HoveringOver();
			}
			else if (component.IsHoveringOver)
				component.StoppedHovering();
		}

		public void Clear()
		{
			Console.Clear();
			_currentTop = 0;
		}

		public void WriteLine(string text, ConsoleColor color)
		{
			Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + _currentTop);
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			_currentTop++;
		}
	}
}