using System.Collections.Generic;
using System;
namespace Abitcareer.Business.Models
{
    public class Point
    {
        public Point(double x = 0, double y = 0)
        {
            this.x = (int)Math.Round(x);
            this.y = (int)Math.Round(y);
        }

        public int x;

        public int y;

        public static List<Point> GetZeroList(uint count)
        {
            var list = new List<Point>((int)count);
            for (var i = 0; i < count; i++)
            {
                list.Add(new Point(i));
            }
            return list;
        }
    }
}
