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
        List<List<Point>> result;
        private const int startOfWorking = 1;
        private const int maxYears = 100;
        private const int yearsOfStudying = 6;
        private int monthPriceIndex;
        private int summaryPriceIndex;
        private int monthSalaryIndex;
        private int summarySalaryIndex;

        public ChartsDataProvider()
        {
            result = new List<List<Point>>();
            monthPriceIndex = 0;
            monthSalaryIndex = 0;
            summaryPriceIndex = 0;
            summarySalaryIndex = 0;
        }

        public List<List<Point>> PrepareData(Speciality speciality, short polinom)
        {
            var topMonthPayments = GetMonthPayments((int)speciality.Prices["TopUniversityPrice"]);
            var middleMonthPayments = GetMonthPayments((int)speciality.Prices["MiddleUniversityPrice"]);
            var lowMonthPayments = GetMonthPayments((int)speciality.Prices["LowUniversityPrice"]);
            var topSummaryPayments = GetSummaryPayments((int)speciality.Prices["TopUniversityPrice"]);
            var middleSummaryPayments = GetSummaryPayments((int)speciality.Prices["MiddleUniversityPrice"]);
            var lowSummaryPayments = GetSummaryPayments((int)speciality.Prices["LowUniversityPrice"]);


            result.Add(Point.InitList(topMonthPayments));
            result.Add(Point.InitList(middleMonthPayments));
            result.Add(Point.InitList(lowMonthPayments));
            result.Add(Point.InitList(1));
            result.Add(Point.InitList(1));
            result.Add(Point.InitList(1));
            result.Add(Point.InitList(startOfWorking)); //salaries per month
            result.Add(Point.InitList(startOfWorking)); //salaries summary

            InitPointsOnPayments(0, 3);
            InitPointsOnPayments(1, 4);
            InitPointsOnPayments(2, 5);
            monthPriceIndex = 0;
            summaryPriceIndex = 3;
            monthSalaryIndex = 6;
            summarySalaryIndex = 7;

            InitSalaries(speciality, polinom);

            return result;
        }



        private void InitPointsOnPayments(int monthPaymentsIndex, int summaryPaymentsIndex)
        {
            var i = 0;
            foreach (var point in result[monthPaymentsIndex])
            {
                var newPoint = new Point(i+1, result[summaryPaymentsIndex][i++].y + 12 * point.y);
                result[summaryPaymentsIndex].Add(newPoint);
            }
            result[summaryPaymentsIndex].Remove(result[summaryPaymentsIndex].Last());
        }

        private int[] GetMonthPayments(int price)
        {
            var payments = new int[yearsOfStudying];
            for (int i = 0; i < yearsOfStudying; i++)
            {
                payments[i] = price / 12;
            }
            return payments;
        }

        private int[] GetSummaryPayments(int price)
        {
            var payments = new int[yearsOfStudying];
            for (int i = 0; i < yearsOfStudying; i++)
            {
                payments[i] = price * (i + 1);
            }
            return payments;
        }

        private void InitSalaries(Speciality speciality, short polinom)
        {
            var aproximator = InitAproximator(speciality, polinom);

            for (var i = startOfWorking; (result[monthPriceIndex].Count > i || result[summarySalaryIndex][i - 1].y <= result[summaryPriceIndex][result[summaryPriceIndex].Count - 1].y) && i < maxYears; i++)
            {
                var val = aproximator.CalcY(i);
                if (val < 0)
                    val = 0;
                Point newPoint;
                newPoint = new Point(i, val);
                if (result[monthPriceIndex].Count > i)
                    result[monthSalaryIndex].Add(newPoint);
                else
                {
                    if (result[monthPriceIndex].Count == i && result[monthPriceIndex].Last().y >= val)
                        result[monthSalaryIndex].Add(newPoint);
                }
                newPoint = new Point(i, result[summarySalaryIndex].Last().y + 12 * val);
                result[summarySalaryIndex].Add(newPoint);
            }
        }

        private Approximator InitAproximator(Speciality speciality, short polinom)
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
                polinom = (short)(x.Count - 1);

            return new Approximator(x, y);
        }

    }
}
