﻿using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abitcareer.Business.Components.ChartsData
{
    public class Approximator
    {
        private float avgDelta { get; set; }
        public List<int> data { get; set; }
        private List<float> Deltas { get; set; }
        private void CalcDelta()
        {
            if (data.Count == 1)
            {
                avgDelta = data[0];
                return;
            }
            avgDelta = data[0] - data[1];
            for (int i = 1; i < data.Count - 1; i++)
            {
                var localDelta = (data[i] - data[i + 1]);
                avgDelta = (avgDelta - localDelta) / 2;
            }
        }
        public Approximator(int[] input)
        {
            data = new List<int>(input);
            Deltas = new List<float>();
            CalcDelta();
        }
        public Approximator(List<int> x, List<int> y)
        {
            Deltas = new List<float>();
            data = new List<int>();
            var counter = 0;
            foreach (var item in x)
            {
                var multiplier = 1;
                var step = y[counter] / item;
                while (data.Count + 1 < item)
                {
                    data.Add((int)(step*1.5) + step * multiplier);
                    multiplier++;
                }
                data.Add(y[counter]);
                counter++;
            }
            CalcDelta();
        }
        public int CalcY(int pos = -1)
        {
            if (pos == -1)
            {
                var lastDelta = data.LastOrDefault() - data[data.Count - 2];
                var shift = lastDelta == 0 ? 0 : (avgDelta + (avgDelta / lastDelta));
                Deltas.Add(shift);
                data.Add(data.LastOrDefault() + (int)Math.Floor(shift));
                CalcDelta();
                return data.LastOrDefault();
            }
            else
            {
                while (pos > data.Count)
                {
                    var lastDelta = (data.Count - 2 >=0) ? data.LastOrDefault() - data[data.Count - 2] : 0;
                    var shift = lastDelta == 0 ? 0 : (avgDelta + (avgDelta / (avgDelta + lastDelta)));
                    Deltas.Add(shift);
                    data.Add(data.LastOrDefault() + (int)Math.Floor(shift));
                    CalcDelta();
                }
                return data[pos - 1];
            }
        }
    }
}
