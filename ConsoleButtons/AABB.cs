namespace ConsoleButtons
{
	public struct AABB
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;

		public AABB(int x, int y, int w, int h)
		{
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		public bool Contains(int column, int row)
		{
			return column >= X && column < X + Width
			                   && row >= Y && row < Y + Height;
		}
	}
}