using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;

namespace Abitcareer.Business.Components
{
    public class Approximator
    {
        private double[] Coef;
        private double[] UnknownCoef;
        private int Polinom;
        private double[,] Matrix;
        private int[] x;
        private int[] y;

        public Approximator(List<int> x, List<int> y, short polinom)
        {
            if (x.Count != y.Count)
            {
                throw new ArgumentException("different arrays length", "x y");
            }
            if (polinom >= x.Count)
            {
                throw new ArgumentException("can't solve: polinom power to large", "polinom");
            }
            Matrix = new double[x.Count, y.Count];
            Polinom = polinom;
            UnknownCoef = new double[polinom + 1];
            Coef = new double[polinom + 1];
            this.x = x.ToArray();
            this.y = y.ToArray();
            GetCoef();
        }

        private void InitMatrix()
        {
            for (var i = 0; i < Polinom + 1; i++)
            {
                for (var j = 0; j < Polinom + 1; j++)
                {
                    Matrix[i, j] = 0;
                    for (var k = 0; k < x.Length; k++)
                    {
                        Matrix[i, j] += Math.Pow(x[k], i + j);
                    }
                }
            }

            for (var i = 0; i < Polinom + 1; i++)
            {
                for (var k = 0; k < x.Length; k++)
                {
                    Coef[i] += Math.Pow(x[k], i) * y[k];
                }
            }
        }

        private void Diagonal()
        {
            int i, j, k;
            double temp = 0;
            for (i = 0; i < Polinom + 1; i++)
            {
                if (Matrix[i, i] != 0)
                    continue;        
                for (j = 0; j < Polinom + 1; j++)
                {
                    if (j == i)
                        continue;
                    if (Matrix[j, i] != 0 && Matrix[i, j] != 0)
                    {
                        for (k = 0; k < Polinom + 1; k++)
                        {
                            temp = Matrix[j, k];
                            Matrix[j, k] = Matrix[i, k];
                            Matrix[i, k] = temp;
                        }
                        temp = Coef[j];
                        Coef[j] = Coef[i];
                        Coef[i] = temp;
                        break;
                    }
                }
            }
        }

        private void ProcessRows()
        {
            for (var k = 0; k < Polinom + 1; k++)
            {
                for (var i = k + 1; i < Polinom + 1; i++)
                {
                    if (Matrix[k, k] == 0)
                    {
                        return;
                    }
                    double M = Matrix[i, k] / Matrix[k, k];
                    for (var j = k; j < Polinom + 1; j++)
                    {
                        Matrix[i, j] -= M * Matrix[k, j];
                    }
                    Coef[i] -= M * Coef[k];
                }
            }
        }

        private void FindCoef()
        {
            for (var i = Polinom; i >= 0; --i)
            {
                double s = 0;
                for (var j = i; j < Polinom + 1; j++)
                {
                    s = s + Matrix[i, j] * UnknownCoef[j];
                }
                UnknownCoef[i] = (Coef[i] - s) / Matrix[i, i];
            }
        }

        public double[] GetCoef()
        {
            InitMatrix();
            Diagonal();
            ProcessRows();
            FindCoef();
            return UnknownCoef;
        }

        public double CalcY(double x)
        {
            double y = 0, mult = 1;;
            foreach(var coef in UnknownCoef)
            {
                y += coef * mult;
                mult *= x;
            }
            return y;
        }

        public List<Point> CalcY(List<double> x)
        {
            var res = new List<Point>(x.Count);
            foreach (var m in x)
            {
                res.Add(new Point(m, CalcY(m)));
            }
            return res;
        }

        public List<Point> CalcY(double fromX, double toX)
        {
            if (toX < fromX)
            {
                throw new ArgumentException("uncorrect range");
            }
            var res = new List<Point>((int)(toX - fromX));
            for (; fromX <= toX; fromX++)
            {
                res.Add(new Point(fromX, CalcY(fromX)));
            }
            return res;
        }
    }
}
