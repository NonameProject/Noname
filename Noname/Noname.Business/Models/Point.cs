
using System;
namespace Abitcareer.Business.Models
{
    public class Point
    {
        public Point(double x, double y)
        {
            this.x = (int)Math.Round(x);
            this.y = (int)Math.Round(y);
        }

        public int x;

        public int y;
    }
}
