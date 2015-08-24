using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abitcareer.Business.Components.ChartsData.Approximation
{
    public class Approximator : IApproximator
    {
        private int startOfWorking { get; set; }
        private double avgDelta { get; set; }
        public Dictionary<int, double> data { get; set; }
        private List<double> Deltas { get; set; }
        private void CalcDelta()
        {
            var tmp = data.Values.ToList();
            if (tmp.Count == 1)
            {
                avgDelta = tmp[0];
                return;
            }
            avgDelta = tmp[0] - tmp[1];
            for (int i = 1; i < tmp.Count - 1; i++)
            {
                var localDelta = (tmp[i] - tmp[i + 1]);
                avgDelta = (avgDelta - localDelta) / 2;
            }
        }
        public Approximator(List<int> x, List<int> y, int startOfWorking = 0)
        {
            Deltas = new List<double>();
            data = new Dictionary<int, double>();
            var counter = 0;
            var previous = 0;
            var year = 1;
            if (startOfWorking == 0)
            {
                year = x.First();
                this.startOfWorking = year;
            }
            else
            {
                this.startOfWorking = startOfWorking;
            }
            foreach (var item in x)
            {
                var shiftedYear = item + startOfWorking;
                var delta = (y.Count < 2 || counter == 0) ? y[counter] : y[counter] - y[counter - 1];
                var step = (shiftedYear + 1 == year) ? 0 : delta / (shiftedYear - (year - 1));
                while (year < shiftedYear)
                {
                    var valueToAdd = (counter == 0) ? 0 : previous + step;
                    data.Add(year - 1, valueToAdd);
                    previous = previous + step;
                    year++;
                }
                if (!data.ContainsKey(shiftedYear))
                {
                    data.Add(shiftedYear - 1, y[counter]);
                    previous = y[counter];
                    year = shiftedYear + 1;
                }
                counter++;
            }
            CalcDelta();
            
        }
        public double CalcY(double x)
        {
            if (x < startOfWorking)
                return 0;
            var convertedX = (int)Math.Floor(x);
            if (!data.ContainsKey(convertedX))
            {
                var lastDelta = (data.ContainsKey(convertedX - 1) && data.ContainsKey(convertedX - 2)) ? data[convertedX - 1] - data[convertedX - 2] : 0;
                var shift = lastDelta == 0 ? 0 : (avgDelta + (avgDelta / (avgDelta + lastDelta)));
                Deltas.Add(shift);
                data.Add(convertedX, data.LastOrDefault().Value + (int)Math.Floor(shift));
                CalcDelta();
            }
            return data[convertedX];
        }
    }
}
