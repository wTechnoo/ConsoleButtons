using System;

namespace ConsoleButtons
{
    public class AABB
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
    }
}
