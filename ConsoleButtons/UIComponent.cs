using System;
using System.Drawing;

namespace ConsoleButtons
{
	public abstract class UIComponent
	{
		/// <summary>Collider, in console character cells. Measured, never hand-tuned.</summary>
		public AABB AABB;

		public Point ConsolePosition;
		public bool IsHoveringOver;

		public event Action OnClick;
		public event Action OnHold;
		public event Action OnHoverOver;
		public event Action OnHoverStop;

		/// <summary>
		/// Exactly what this component paints. Single source of truth: the collider is
		/// measured from this, so the two can never drift apart.
		/// </summary>
		protected abstract string Render();

		/// <summary>The clickable part of the render. Defaults to all of it.</summary>
		protected virtual string HitArea()
		{
			return Render();
		}

		/// <summary>Re-measures the collider from what the component currently paints.</summary>
		public void Recalculate()
		{
			var (width, height) = TextMetrics.Measure(HitArea());
			AABB = new AABB(ConsolePosition.X, ConsolePosition.Y, width, height);
		}

		public void WriteWithNoColor()
		{
			WriteWithColor(ConsoleColor.White);
		}

		public void WriteWithColor(ConsoleColor color)
		{
			Console.ForegroundColor = color;

			var lines = Render().Replace("\r\n", "\n").Split('\n');

			for (var i = 0; i < lines.Length; i++)
				WriteAt(ConsolePosition.X, ConsolePosition.Y + i, lines[i]);

			Recalculate();
		}

		/// <summary>
		/// Console.Write, never WriteLine: a newline printed on the bottom row scrolls the
		/// whole buffer up and leaves every other component one row out of place.
		/// </summary>
		protected static void WriteAt(int column, int row, string text)
		{
			// Nothing to position against when the output is piped to a file or another
			// process, and asking for the buffer size would throw.
			if (Console.IsOutputRedirected)
				return;

			if (column < 0 || row < 0 || column >= Console.BufferWidth || row >= Console.BufferHeight)
				return;

			Console.SetCursorPosition(column, row);
			Console.Write(text);
		}

		public virtual void Update()
		{
		}

		public virtual void Clicked()
		{
			OnClick?.Invoke();
		}

		public virtual void Hold()
		{
			OnHold?.Invoke();
		}

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
}