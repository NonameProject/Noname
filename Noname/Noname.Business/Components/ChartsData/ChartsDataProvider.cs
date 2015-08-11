using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.ChartsData
{
    public class ChartsDataProvider
    {        
        public List<List<Point>> PrepareData(Speciality speciality, short polinom)
        {                        
            const int startOfWorking = 3;
            int i = 0;

            var aproximator = initAproximator(speciality, polinom);

            var payment = new int[] { 1200, 1200, 1200, 1200, 1200, 1400 };
            var res = new List<List<Point>>(4);
            res.Add(Point.InitList(payment));               //wages per month
            res.Add(Point.InitList(startOfWorking));        //earning per month
            res.Add(Point.InitList(1));                     //total wages
            res.Add(Point.InitList(startOfWorking + 1));    //total earning

            i = 0;
            foreach (var point in res[0])
            {
                var newPoint = new Point(i + 1, res[2][i++].y + 12 * point.y);
                res[2].Add(newPoint);
            }            

            for (i = startOfWorking; res[0].Count > i || res[3][i].y <= res[2][res[2].Count - 1].y; i++)
            {
                var val = aproximator.CalcY(i);
                Point newPoint;
                newPoint = new Point(i, val);
                res[1].Add(newPoint);
                newPoint = new Point(i + 1, res[3][res[3].Count - 1].y + 12 * val);
                res[3].Add(newPoint);
            }

            return res;
        }

        private Approximator initAproximator(Speciality speciality, short polinom)
        {
            List<int> x, y;
            x = new List<int>();
            y = new List<int>();

            foreach (var key in speciality.Salaries.Keys)
            {
                int val;
                speciality.Salaries.TryGetValue(key, out val);
                if (val > 0 || key == 1)
                {
                    x.Add(key);
                    y.Add(val);
                }
            }
            if (polinom + 1 >= x.Count)
                polinom = (short)( x.Count - 1 );

            return new Approximator(x, y, polinom);
        }
        
    }
}
