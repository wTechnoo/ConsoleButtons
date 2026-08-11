using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ConsoleButtons
{
	/// <summary>
	/// Translates between screen pixels and console character cells.
	/// Nothing here is hardcoded: the cell size is measured from the window itself, so it
	/// stays correct across font changes, DPI scaling and window resizes.
	/// </summary>
	public static class ConsoleMetrics
	{
		const float FallbackCellWidth = 8f;
		const float FallbackCellHeight = 16f;

		static IntPtr handle;

		/// <summary>
		/// Handle of the console window. More reliable than Process.MainWindowHandle, which
		/// returns Zero in a few launch scenarios.
		/// </summary>
		public static IntPtr Handle
		{
			get
			{
				if (handle == IntPtr.Zero)
					handle = GetConsoleWindow();

				return handle;
			}
		}

		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		static extern bool ScreenToClient(IntPtr hWnd, ref MouseExt.POINT lpPoint);

		/// <summary>
		/// Size of one character cell in screen pixels. Measured by dividing the window's
		/// client area (which already excludes the title bar and the borders) by how many
		/// cells the console says it is showing.
		/// </summary>
		public static (float width, float height) CellSize()
		{
			var columns = Console.WindowWidth;
			var rows = Console.WindowHeight;

			RECT client;

			if (Handle == IntPtr.Zero || columns <= 0 || rows <= 0
			    || !GetClientRect(Handle, out client)
			    || client.Right <= client.Left || client.Bottom <= client.Top)
				return (FallbackCellWidth, FallbackCellHeight);

			return ((client.Right - client.Left) / (float)columns,
				(client.Bottom - client.Top) / (float)rows);
		}

		/// <summary>Mouse position in pixels relative to the top-left of the text area.</summary>
		public static (int x, int y) MouseClientPixels()
		{
			var point = MouseExt.GetCursorPositionRaw();

			if (Handle != IntPtr.Zero)
				ScreenToClient(Handle, ref point);

			return (point.X, point.Y);
		}

		/// <summary>
		/// Mouse position in console cell coordinates, keeping the fraction inside the cell.
		/// Useful for smooth dragging.
		/// </summary>
		public static (float column, float row) MouseCellPrecise()
		{
			var (x, y) = MouseClientPixels();
			var (cellWidth, cellHeight) = CellSize();

			return (x / cellWidth + Console.WindowLeft, y / cellHeight + Console.WindowTop);
		}

		/// <summary>
		/// Mouse position snapped to the cell it is over, in the same coordinate space as
		/// Console.SetCursorPosition.
		/// </summary>
		public static (int column, int row) MouseCell()
		{
			var (column, row) = MouseCellPrecise();

			return ((int)Math.Floor(column), (int)Math.Floor(row));
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left, Top, Right, Bottom;
		}
	}

	/// <summary>
	/// Measures strings in console cells, so a component's collider can be derived from what
	/// it actually paints instead of being tuned by hand.
	/// </summary>
	public static class TextMetrics
	{
		/// <summary>Width of the longest line, and the number of lines.</summary>
		public static (int width, int height) Measure(string text)
		{
			if (string.IsNullOrEmpty(text))
				return (0, 1);

			var lines = text.Replace("\r\n", "\n").Split('\n');

			var width = 0;

			for (var i = 0; i < lines.Length; i++)
			{
				var lineWidth = DisplayWidth(lines[i]);

				if (lineWidth > width)
					width = lineWidth;
			}

			return (width, lines.Length);
		}

		/// <summary>
		/// How many cells the console actually paints for this string. Not the same as
		/// string.Length: an emoji is two UTF-16 units but paints two cells, while a CJK
		/// glyph is one unit and also paints two.
		/// </summary>
		public static int DisplayWidth(string text)
		{
			if (string.IsNullOrEmpty(text))
				return 0;

			var width = 0;
			var elements = StringInfo.GetTextElementEnumerator(text);

			while (elements.MoveNext())
				width += IsFullWidth((string)elements.Current) ? 2 : 1;

			return width;
		}

		static bool IsFullWidth(string element)
		{
			if (string.IsNullOrEmpty(element))
				return false;

			// Not ConvertToUtf32(element, 0): that throws on an unpaired surrogate.
			var codePoint = char.IsHighSurrogate(element[0]) && element.Length > 1 && char.IsLowSurrogate(element[1])
				? char.ConvertToUtf32(element[0], element[1])
				: element[0];

			return (codePoint >= 0x1100 && codePoint <= 0x115F) // Hangul Jamo
			       || (codePoint >= 0x2E80 && codePoint <= 0x303E) // CJK radicals, Kangxi
			       || (codePoint >= 0x3041 && codePoint <= 0x33FF) // Kana, Hangul compat
			       || (codePoint >= 0x3400 && codePoint <= 0x4DBF) // CJK extension A
			       || (codePoint >= 0x4E00 && codePoint <= 0x9FFF) // CJK unified
			       || (codePoint >= 0xA000 && codePoint <= 0xA4CF) // Yi
			       || (codePoint >= 0xAC00 && codePoint <= 0xD7A3) // Hangul syllables
			       || (codePoint >= 0xF900 && codePoint <= 0xFAFF) // CJK compat ideographs
			       || (codePoint >= 0xFE30 && codePoint <= 0xFE6F) // CJK compat forms
			       || (codePoint >= 0xFF00 && codePoint <= 0xFF60) // Fullwidth forms
			       || (codePoint >= 0xFFE0 && codePoint <= 0xFFE6)
			       || (codePoint >= 0x1F300 && codePoint <= 0x1F64F) // Emoji
			       || (codePoint >= 0x1F900 && codePoint <= 0x1F9FF)
			       || (codePoint >= 0x20000 && codePoint <= 0x3FFFD); // CJK extension B and up
		}
	}
}