namespace ConsoleButtons
{
	public struct AABB
	{
		public int x, y;
		public int width, height;

		public AABB(int x, int y, int w, int h)
		{
			this.x = x;
			this.y = y;
			width = w;
			height = h;
		}

		public bool Contains(int column, int row)
		{
			return column >= x && column < x + width
			                   && row >= y && row < y + height;
		}
	}
}