using System;
using System.Drawing;

namespace ConsoleButtons
{
	public class Button : UIComponent
	{
		string _text;

		public Button(string text, int x, int y)
		{
			_text = text;
			ConsolePosition = new Point(x, y);
		}

		[Obsolete("The collider is now measured from the text automatically; w and h are ignored. Use Button(text, x, y).")]
		public Button(string text, int x, int y, int w, int h) : this(text, x, y)
		{
		}

		public string Text
		{
			get => _text;
			set
			{
				_text = value;
				Recalculate();
			}
		}

		protected override string Render()
		{
			return _text;
		}
	}
}