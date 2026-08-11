using System;
using System.Drawing;

namespace ConsoleButtons
{
	public class Button : UIComponent
	{
		string text;

		public Button(string text, int x, int y)
		{
			this.text = text;
			ConsolePosition = new Point(x, y);
		}

		[Obsolete("The collider is now measured from the text automatically; w and h are ignored. Use Button(text, x, y).")]
		public Button(string text, int x, int y, int w, int h) : this(text, x, y)
		{
		}

		public string Text
		{
			get => text;
			set
			{
				text = value;
				Recalculate();
			}
		}

		protected override string Render()
		{
			return text;
		}
	}
}