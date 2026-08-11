using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleButtons
{
	public static class Window
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string strClassName, string strWindowName);

		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

		public static int Remap(int value, int low1, int high1, int low2, int high2)
		{
			return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
		}

		public static float Remap(float value, float low1, float high1, float low2, float high2)
		{
			return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
		}

		// Kept for compatibility. These no longer guess at the font size or the height of
		// the title bar — they go through ConsoleMetrics, which measures both.
		// WindowRect is ignored.

		[Obsolete("Components are positioned in console cells now. Use ConsoleMetrics.CellSize if you need pixels.")]
		public static (int x, int y) ConvertConsoleToPx(int X, int Y, Rect WindowRect)
		{
			var (cellWidth, cellHeight) = ConsoleMetrics.CellSize();

			return ((int)((X - Console.WindowLeft) * cellWidth),
				(int)((Y - Console.WindowTop) * cellHeight));
		}

		[Obsolete("Components are positioned in console cells now. Use ConsoleMetrics.MouseCell for the cursor.")]
		public static (int x, int y) ConvertPxToConsole(int X, int Y, Rect WindowRect)
		{
			var (cellWidth, cellHeight) = ConsoleMetrics.CellSize();

			return ((int)(X / cellWidth) + Console.WindowLeft,
				(int)(Y / cellHeight) + Console.WindowTop);
		}

		public struct Rect
		{
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }
		}
	}

	public static class MouseExt
	{
		public const int MousePressed = 0x8000;

		[DllImport("user32.dll")]
		public static extern int GetAsyncKeyState(VK vKeys);

		[DllImport("user32.dll")]
		static extern bool GetCursorPos(out POINT lpPoint);

		public static Point GetCursorPosition()
		{
			return GetCursorPositionRaw();
		}

		/// <summary>Screen-space cursor position, in the form ScreenToClient expects.</summary>
		public static POINT GetCursorPositionRaw()
		{
			POINT lpPoint;
			GetCursorPos(out lpPoint);

			return lpPoint;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public static implicit operator Point(POINT point)
			{
				return new Point(point.X, point.Y);
			}
		}
	}

	public enum VK : int
	{
		LBUTTON = 0x01,
		RBUTTON = 0x02,
		MBUTTON = 0x04
	}

	public static class DisableConsoleQuickEdit
	{
		const uint ENABLE_QUICK_EDIT = 0x0040;

		// STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
		const int STD_INPUT_HANDLE = -10;

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll")]
		static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		[DllImport("kernel32.dll")]
		static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

		internal static bool Go()
		{
			var consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

			// get current console mode
			uint consoleMode;

			if (!GetConsoleMode(consoleHandle, out consoleMode))
			{
				// ERROR: Unable to get console mode.
				return false;
			}

			// Clear the quick edit bit in the mode flags
			consoleMode &= ~ENABLE_QUICK_EDIT;

			// set the new mode
			if (!SetConsoleMode(consoleHandle, consoleMode))
			{
				// ERROR: Unable to set console mode
				return false;
			}

			return true;
		}
	}
}