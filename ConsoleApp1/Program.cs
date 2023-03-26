using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var P = new Program();
            P.Solve();

        }

        void Solve()
        {
            const double a = -5.0, b = 5.0, eps = 0.1;
            List<double> interval = new List<double> { a, b };
            double l = L(eps, interval);

            int k = 1;

            double Result = 1;

            int Min_I = 0;

            while (Result > eps)
            {
                Console.WriteLine("/subsection{Шаг: " + k + "}");
                var R = new List<double>();
                //характеристики
                for (var i = 0; i < interval.Count - 1; i++)
                {
                    Result = ((f(interval[i]) + f(interval[i + 1])) / 2) - l * ((interval[i + 1] - interval[i]) / 2);
                    R.Add(Result);
                }
                //интервал с минимальной R

                for (var i = 0; i < R.Count; i++)
                {
                    if (R[i] < R[Min_I]) Min_I = i;
                }

                Console.WriteLine("Интервалы: ");
                for (var i = 0; i < interval.Count - 1; i++)
                {
                    Console.WriteLine("$$[" + interval[i] + ";" + interval[i + 1] + "]$$");
                }

                Result = Math.Abs(interval[Min_I + 1] - interval[Min_I]);
                Console.WriteLine("Характеристики: ");

                for (var i = 0; i < R.Count; i++)
                {
                    Console.WriteLine("$$R(" + i + ") = " + R[i] + "$$");
                }

                if (Result > eps)
                {

                    Console.WriteLine("Точка останова не выполняется: $$ |" + interval[Min_I + 1] + " - " + interval[Min_I] + "| = " + Result + " >= /epsilon $$");
                    Console.WriteLine("Наименьшая характеристика у интервала: ");
                    Console.WriteLine("$$");
                    Console.WriteLine("[" + interval[Min_I] + ";" + interval[Min_I + 1] + "]");
                    Console.WriteLine("$$");

                    Console.WriteLine("Наименьшая характеристика");
                    Console.WriteLine("$$");
                    Console.WriteLine("R = " + R[Min_I]);
                    Console.WriteLine("$$");

                    interval.Add(((interval[Min_I + 1] + interval[Min_I]) / 2 - (f(interval[Min_I + 1]) - f(interval[Min_I])) / 2) / l);
                    interval.Sort();
                }
                else
                {

                    Console.WriteLine("Точка останова выполняется: $$ |" + interval[Min_I + 1] + " - " + interval[Min_I] + "| = " + Result + " <= /epsilon $$ ");
                    Console.WriteLine("Интервалы: ");

                    for (var i = 0; i < interval.Count - 1; i++)
                    {
                        Console.WriteLine("$$[" + interval[i] + ";" + interval[i + 1] + "]$$");
                    }


                    for (var i = 0; i < R.Count; i++)
                    {
                        Console.WriteLine("$$R(" + i + ") = " + R[i] + "$$");
                    }

                    double F_min = 10000;
                    for (var i = 0; i < R.Count; i++)
                    {
                        if (R[i] < F_min) F_min = R[i];
                    }
                    Console.WriteLine("$$");
                    Console.WriteLine("f* = " + F_min);
                    Console.WriteLine("x* = " + interval[Min_I]);
                    Console.WriteLine("$$");
                }

                k++;
            }
            Console.ReadKey();
        }

        double f(double x)
        {
            //return 2 * Math.Atan(x) + 1 - Math.Sin(3 * x);
            //return Math.Atan(x) + x - 3 + Math.Acos(x);
            return Math.Atan(x) + 2.5;
        }

        double f_proizv(double x)
        {
            //return 2/(1 + Math.Pow(x, 2)) - 3 * Math.Cos(3 * x);
            //return 1 / (1 + Math.Pow(x, 2)) + 1 - 1 / Math.Sqrt(1 - Math.Pow(x, 2));
            return 1 / (1 + Math.Pow(x, 2));
        }

        double L(double eps, List<double> interval)
        {
            double max = -10000;
            for (double i = interval[0]; i < interval[1]; i += 0.0001)
            {
                if (f_proizv(i) > max) max = f_proizv(i);
            }
            return max;
        }
    }
}
