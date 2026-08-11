using System;
using System.Drawing;

namespace ConsoleButtons
{
	public class Slider : UIComponent
	{
		public float Value;

		char _fillChar, _unfilledChar;

		float _maxValue;

		int _size;
		bool _toInt;

		public Slider(float initialValue, float maxValue, int size, bool toInt, char fillChar, char unfilledChar, int x, int y)
		{
			Value = initialValue;

			_fillChar = fillChar;
			_toInt = toInt;
			_unfilledChar = unfilledChar;
			_size = size;
			_maxValue = maxValue;

			ConsolePosition = new Point(x, y);
		}

		protected override string Render()
		{
			var bar = new char[_size + 2];
			bar[0] = '[';
			bar[_size + 1] = ']';

			var filled = _maxValue <= 0f ? 0f : Value / _maxValue * _size;

			for (var i = 0; i < _size; i++)
				bar[i + 1] = i < filled ? _fillChar : _unfilledChar;

			return new string(bar);
		}

		public override void Hold()
		{
			var (column, _) = ConsoleMetrics.MouseCellPrecise();

			var t = _size <= 0 ? 0f : (column - (ConsolePosition.X + 1)) / _size;
			if (t < 0f) t = 0f;
			if (t > 1f) t = 1f;

			Value = _toInt ? (float)Math.Round(t * _maxValue) : t * _maxValue;

			base.Hold();
		}

		public override void Update()
		{
			if (Value <= 0)
				Value = 0;

			if (Value >= _maxValue)
				Value = _maxValue;
		}
	}
}