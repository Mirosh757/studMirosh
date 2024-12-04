using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace terver1
{
    internal class Terver
    {
        Appendix Appendix = new Appendix();
        Dictionary<double, int> variant = new Dictionary<double, int>();
        int n = 0;
        public int N { set { } get { return n; } }
        public Dictionary<double, int> Addter()
        {
            Dictionary<double, int> variant1 = new Dictionary<double, int>();
            string s = Console.ReadLine();
            string[] data = s.Split(' ');
            int k = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (variant1.ContainsKey(Double.Parse(data[i])))
                {
                    variant1[Double.Parse(data[i])]++;
                }
                else
                    variant1.Add(Double.Parse(data[i]), 1);
                k++;
            }
            int q = variant1.Count;
            for(int i = 0;i < q;i++)
            {
                double min = 99999;
                int n = 0;
                foreach(var a in variant1)
                {
                    if (a.Key < min)
                    {
                        min = a.Key;
                        n = a.Value;
                    }
                }
                variant1.Remove(min);
                variant.Add(min, n);
            }
            n = k;
            Console.WriteLine(k);
            return variant;
        }
        public Dictionary<double, int> Addter(double[] data)
        {
            variant = new Dictionary<double, int>();
            Dictionary<double, int> variant1 = new Dictionary<double, int>();
            int k = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (variant1.ContainsKey(data[i]))
                {
                    variant1[data[i]]++;
                }
                else
                    variant1.Add(data[i], 1);
                k++;
            }
            int q = variant1.Count;
            for (int i = 0; i < q; i++)
            {
                double min = 99999;
                int n = 0;
                foreach (var a in variant1)
                {
                    if (a.Key < min)
                    {
                        min = a.Key;
                        n = a.Value;
                    }
                }
                variant1.Remove(min);
                variant.Add(min, n);
            }
            n = k;
            Console.WriteLine($"Кол-во элементов: {k}");
            return variant;
        }
        public void AddDictonary(Dictionary<double, int> variant1)
        {
            variant = variant1;
            n = 0;
            foreach(var a in variant1)
            {
                n += a.Value;
            }
        }
        public void Print()
        {
            foreach (var a in variant)
                Console.Write(a.ToString());
        }
        public void PrintPeriodicity()
        {
            double sum = 0;
            foreach (var a in variant)
            {
                sum += a.Value / (n + 0.0);
                Console.Write($"[{a.Key} {a.Value / (n+0.0)}] ");
            }
            Console.WriteLine($"\nПроверка относительных частот: {sum}");
        }
        public double SampleAverage()
        {
            double sum = 0;
            foreach(var a in variant)
            {
                sum += a.Key * a.Value;
            }
            return sum / n;
        }
        public double SampleAverage(Dictionary<double,int> variant1)
        {
            double sum = 0, n1 = 0;
            foreach (var a in variant1)
            {
                sum += a.Key * a.Value;
                n1 += a.Value;
            }
            return sum / n1;
        }
        public double[] SampleVariant(double[] x, double[] y)
        {
            double[] xy = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                xy[i] = (x[i] + y[i]) / 2;
            return xy;
        }
        public double SampleAverage(double[] x)
        {
            double sum = 0, n1 = x.Length;
            for (int i = 0; i < x.Length; i++)
            {
                sum += x[i];
            }
            return sum / n1;
        }
        public double SampleAverage(double[] x, double[] y)
        {
            double sum = 0, n1 = x.Length;
            for(int i = 0;i < x.Length;i++)
            {
                sum += (x[i] * y[i]);
            }
            return sum / n1;
        }
        public double Dispersion(double sv)
        {
            double sum = 0;
            foreach(var a in variant)
            {
                sum += Math.Pow(a.Key - sv, 2) * a.Value;
            }
            return sum / n;
        }
        public double Deviation(double dispersion)
        {
            return Math.Sqrt(dispersion);
        }
        public double CoefficientVariation(double sv, double deviation)
        {
            return deviation / sv * 100;
        }
        public double CorrecteDispersion(double dispersion)
        {
            return n / (n - 1) * dispersion;
        }
        public double CorrecteDeviation(double correcteDispersion)
        {
            return Math.Sqrt(correcteDispersion);
        }
        public string ConfidenceIntervalMathematicalExpectation(double xv, double dev, double a)
        {
            double xy = Appendix.GetKeyLaplaceFunction(a/2);
            double y = xy * dev / Math.Sqrt(n);
            return $"{xv - y} < X < {xv + y}";
        }
        public string ConfidenceIntervalDeviationExpectation(double dis, double a)
        {
            double xleft = Appendix.GetCriticalPointsPearsonDistribution(n - 1, (1 + a) / 2);
            double xright = Appendix.GetCriticalPointsPearsonDistribution(n - 1, (1 - a) / 2);
            return $"({Math.Sqrt(n - 1) * dis / xright}; {Math.Sqrt(n - 1) * dis / xleft})";
        }
        public double[] ChancheNorm(double[] intervalRow, double sampleAverage, double deviation)
        {
            double[] np = new double[intervalRow.Length];
            np[0] = (Appendix.GetLaplaceFunction((intervalRow[0] - sampleAverage) / deviation) - Appendix.GetLaplaceFunction(-6)) * n;
            Console.WriteLine($"{Appendix.GetLaplaceFunction((intervalRow[0] - sampleAverage) / deviation)} - {Appendix.GetLaplaceFunction(-6)} = {np[0]}");
            double sum = np[0];
            for(int i = 1;i < np.Length;i++)
            {
                np[i] = (Appendix.GetLaplaceFunction((intervalRow[i] - sampleAverage) / deviation) - Appendix.GetLaplaceFunction((intervalRow[i - 1] - sampleAverage) / deviation)) * n;
                Console.WriteLine($"{Appendix.GetLaplaceFunction((intervalRow[i] - sampleAverage) / deviation)} - {Appendix.GetLaplaceFunction((intervalRow[i - 1] - sampleAverage) / deviation)} = {np[i]}");
                sum += np[i];
            }
            Console.WriteLine(sum);
            return np;
        }
        public double[] ChanchePokaz(double[] intervalRow, double lamda)
        {
            double[] np = new double[intervalRow.Length];
            double sum = 0;
            for(int i = 0;i < np.Length - 1;i++)
            {
                np[i] = n * (Math.Exp(-lamda * intervalRow[i]) - Math.Exp(-lamda * intervalRow[i + 1]));
                Console.WriteLine(np[i]);
                sum += np[i];
            }
            Console.WriteLine(sum);
            return np;
        }
        public double[] ChancheRavnom(double[] intervalRow, double a, double b)
        {
            double[] np = new double[intervalRow.Length];
            double sum = 0;
            np[0] = (n * (intervalRow[0] - a)) / (b - a);
            Console.WriteLine(np[0]);
            sum += np[0];
            for(int i = 1;i < np.Length - 1;i++)
            {
                np[i] = (n * (intervalRow[i] - intervalRow[i - 1])) / (b - a);
                Console.WriteLine(np[i]);
                sum += np[i];
            }
            np[np.Length - 1] = (n * (b - intervalRow[np.Length - 2])) / (b - a);
            Console.WriteLine(np[np.Length - 1]);
            sum += np[np.Length - 1];
            Console.WriteLine(sum);
            return np;
        }
        public void ChiSquareObservable(double[] nv, double[] np, double a, int k)
        {
            double chi = 0;
            for(int i = 0;i < nv.Length;i++)
            {
                chi += Math.Pow(nv[i] - np[i], 2) / np[i];
            }
            Console.WriteLine($"Х^2 набл: {chi}");
            double chikr = Appendix.GetCriticalPointsPearsonDistribution(nv.Length - k, a);
            if(chi < chikr)
                Console.WriteLine($"Х^2 набл < Х^2 крит ({chi} < {chikr}) Гипотеза о нормальном распределении количества инъекций согласуется с опытными данными.");
            else if(chikr == chi)
                Console.WriteLine($"Х^2 набл = Х^2 крит ({chi} = {chikr}) Гипотеза о нормальном распределении количества инъекций не согласуется с опытными данными.");
            else if (chikr < chi)
                Console.WriteLine($"Х^2 набл > Х^2 крит ({chi} > {chikr}) Гипотеза о нормальном распределении количества инъекций не согласуется с опытными данными.");
        }
    }
}
