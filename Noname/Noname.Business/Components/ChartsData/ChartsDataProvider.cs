using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            summaryPriceIndex = 3;
            monthSalaryIndex = 6;
            summarySalaryIndex = 7;
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
            result.Add(new List<Point>(yearsOfStudying)); //salaries per month
            result.Add(new List<Point>(yearsOfStudying)); //salaries summary

            Point point = new Point(speciality.StartOfWorking);
            result[monthSalaryIndex].Add(point);
            result[summarySalaryIndex].Add(point);

            InitPointsOnPayments(0, 3);
            InitPointsOnPayments(1, 4);
            InitPointsOnPayments(2, 5);

            InitSalaries(speciality, polinom);

            return result;
        }



        private void InitPointsOnPayments(int monthPaymentsIndex, int summaryPaymentsIndex)
        {
            var i = 0;
            foreach (var point in result[monthPaymentsIndex])
            {
                result[summaryPaymentsIndex].Add(new Point(i + 1, result[summaryPaymentsIndex][i++].y + 12 * point.y));
            }
            result[summaryPaymentsIndex].Remove(result[summaryPaymentsIndex].Last());
        }

        private int[] GetMonthPayments(int price)
        {
            var payments = new int[yearsOfStudying];
            for (int i = 0; i < yearsOfStudying; i++)
            {
                payments[i] = price / 10;
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

            var isFirstNonZeroValueReached = false;

            for (var i = speciality.StartOfWorking; (result[monthPriceIndex].Count > i ||
                result[summarySalaryIndex][i - 1 - speciality.StartOfWorking].y <= result[summaryPriceIndex][result[summaryPriceIndex].Count - 1].y) && i < maxYears; i++)
            {
                var value = aproximator.CalcY(i);
                if (value < 0)
                    value = 0;

                var newPoint = new Point(i, value);
                if (result[monthPriceIndex].Count > i)
                {
                    if (!isFirstNonZeroValueReached && value > 0) 
                        result[monthSalaryIndex].Add(new Point(i, 0));
                    result[monthSalaryIndex].Add(newPoint);
                }
                else if (result[monthPriceIndex].Count == i && result[monthPriceIndex].Last().y >= value)
                {
                    if (!isFirstNonZeroValueReached && value > 0) 
                        result[monthSalaryIndex].Add(new Point(i, 0));
                    result[monthSalaryIndex].Add(newPoint);
                }

                result[summarySalaryIndex].Add(new Point(i, result[summarySalaryIndex].Last().y + 12 * value));

                if (value > 0)
                    isFirstNonZeroValueReached = true;
            }
        }

        private IApproximator InitAproximator(Speciality speciality, short polinom)
        {
            var x = new List<int>();
            var y = new List<int>();

            var tmp = speciality.Salaries.SkipWhile(pair => pair.Value == -1).ToList();

            foreach (var item in speciality.Salaries)
            {
                int value = item.Value;
                
                   
                if (value >= 0)
                {
                    y.Add(value);
                    x.Add(item.Key);
                }
                else
                {
                    if (item.Key < speciality.Salaries.First(pair => pair.Value > -1).Key)
                    {
                        x.Add(item.Key);
                        y.Add(tmp.First().Value);
                    }
                }
            }
            if (polinom + 1 >= x.Count)
                polinom = (short)(x.Count - 1);

            return new Approximation.Approximator(x, y, speciality.StartOfWorking);
        }

    }
}
