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
            var tmpData = data.OrderBy(x => x.Key).ToDictionary(x=>x.Key, x=>x.Value);
            var tmp = tmpData.TakeWhile(pair => tmpData.ContainsKey(pair.Key + 1)).ToList();
            if (tmp.Count <= 1)
            {
                avgDelta = 0;
                return;
            }
            avgDelta = tmp[1].Value - tmp[0].Value;
            for (int i = 1; i < tmp.Count - 1; i++)
            {
                var localDelta = (tmp[i + 1].Value - tmp[i].Value);
                avgDelta = Math.Floor(((avgDelta - localDelta) / 2) - 0.5);
            }
        }
        public Approximator(List<int> x, List<int> y, int startOfWorking = 0)
        {
            Deltas = new List<double>();
            data = new Dictionary<int, double>();
            var counter = 0;
            var previous = 0;
            var year = startOfWorking;
            this.startOfWorking = startOfWorking;
            foreach (var item in x)
            {
                if (!data.ContainsKey(item))
                {
                    data.Add(item + startOfWorking - 1, y[counter]);
                    previous = y[counter];
                    year = item + startOfWorking + 1;
                    counter++;
                    continue;
                }
                var shiftedYear = item + ((startOfWorking == 0) ? 1 : startOfWorking);
                var delta = (y.Count < 2 || counter == 0) ? y[counter] : y[counter] - y[counter - 1];
                var step = (shiftedYear + 1 == year) ? 0 : delta / (shiftedYear - (year - 1));
                while (year < shiftedYear)
                {
                    var valueToAdd = (counter == 0) ? 0 : previous + step;
                    data.Add(year - 1, valueToAdd);
                    previous = previous + step;
                    year++;
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
            for (int i = 0; i < convertedX; i++)
            {
                if (!data.ContainsKey(i))
                    CalcY(i);
            }
            if (!data.ContainsKey(convertedX))
            {
                if (data.Count(pair => pair.Key > convertedX) > 0)
                {
                    var difference = data.First(pair => pair.Key >= convertedX).Value - data[convertedX - 1];
                    var keyDifference = data.First(pair => pair.Key >= convertedX).Key - (convertedX - 1);
                    var shift = difference / keyDifference;
                    Deltas.Add(shift);
                    data.Add(convertedX, data[convertedX - 1] + (int)Math.Floor(shift));
                    CalcDelta();
                }
                else
                {
                    if (Deltas.Count > 0)
                        data.Add(convertedX, data[convertedX - 1] + (Deltas.LastOrDefault() * Math.Abs(avgDelta / Deltas.LastOrDefault())));
                    else
                        data.Add(convertedX, data[convertedX - 1]);
                }
                    
            }
            return data[convertedX];
        }
    }
}
