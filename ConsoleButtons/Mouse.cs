using System;
using System.Drawing;

namespace ConsoleButtons
{
	public struct Mouse
	{
		/// <summary>Cursor in console cells — the coordinate space colliders live in.</summary>
		public static Point ConsoleMousePoint = new Point(0, 0);

		/// <summary>Cursor in pixels, relative to the top-left of the text area.</summary>
		public static AABB LocalMousePoint = new AABB(0, 0, 2, 2);

		/// <summary>Cursor in screen pixels.</summary>
		public static Point MousePoint = new Point(0, 0);

		public static int PreviousClickState = 0;
		public static int ClickState = 0;
		public static bool Holding = false;
		public static bool Clicked = false;

		public void Update(Window.Rect windowRect)
		{
			ClickState = MouseExt.GetAsyncKeyState(VK.LBUTTON);
			MousePoint = MouseExt.GetCursorPosition();

			Holding = IsHolding();
			Clicked = IsPreviousHolding() && !IsHolding();

			(LocalMousePoint.x, LocalMousePoint.y) = ConsoleMetrics.MouseClientPixels();
			(ConsoleMousePoint.X, ConsoleMousePoint.Y) = ConsoleMetrics.MouseCell();

			PreviousClickState = ClickState;
		}

		public bool IsPreviousHolding()
		{
			return Convert.ToBoolean(PreviousClickState & MouseExt.MousePressed);
		}

		public bool IsHolding()
		{
			return Convert.ToBoolean(ClickState & MouseExt.MousePressed);
		}
	}
}