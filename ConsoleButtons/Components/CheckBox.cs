using System.Drawing;

namespace ConsoleButtons
{
	public class CheckBox : UIComponent
	{
		public bool IsChecked;
		bool collideWithText;
		char markChar;

		string text;

		public CheckBox(string text, char markChar, bool isChecked, int x, int y)
			: this(text, markChar, isChecked, false, x, y)
		{
		}

		public CheckBox(string text, char markChar, bool isChecked, bool collideWithText, int x, int y)
		{
			this.text = text;
			this.markChar = markChar;
			this.collideWithText = collideWithText;
			IsChecked = isChecked;

			ConsolePosition = new Point(x, y);
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
			return IsChecked ? $"[{markChar}] {text}" : $"[ ] {text}";
		}

		// The box alone, unless the caller asked for the label to be clickable too.
		protected override string HitArea()
		{
			return collideWithText ? Render() : "[ ]";
		}

		public override void Clicked()
		{
			base.Clicked();
			IsChecked = !IsChecked;
		}
	}
}