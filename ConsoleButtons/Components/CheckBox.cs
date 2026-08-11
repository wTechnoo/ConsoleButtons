using System.Drawing;

namespace ConsoleButtons
{
	public class CheckBox : UIComponent
	{
		public bool IsChecked;
		bool _collideWithText;
		char _markChar;

		string _text;

		public CheckBox(string text, char markChar, bool isChecked, int x, int y)
			: this(text, markChar, isChecked, false, x, y)
		{
		}

		public CheckBox(string text, char markChar, bool isChecked, bool collideWithText, int x, int y)
		{
			_text = text;
			_markChar = markChar;
			_collideWithText = collideWithText;
			IsChecked = isChecked;

			ConsolePosition = new Point(x, y);
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
			return IsChecked ? $"[{_markChar}] {_text}" : $"[ ] {_text}";
		}

		// The box alone, unless the caller asked for the label to be clickable too.
		protected override string HitArea()
		{
			return _collideWithText ? Render() : "[ ]";
		}

		public override void Clicked()
		{
			base.Clicked();
			IsChecked = !IsChecked;
		}
	}
}