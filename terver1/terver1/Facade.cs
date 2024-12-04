using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terver1
{
    internal class Facade
    {
        Terver terver = new Terver();
        Dictionary<double, int> variant = new Dictionary<double, int>();
        bool flag = false;
        double[] data = null;
        private void CreateObject()
        {
            variant = terver.Addter();
            PrintMenu();
        }
        private void PrintObject()
        {
            if (flag)
                terver.Print();
            else Console.WriteLine("Объект ещё не создан");
            PrintMenu();
        }
        private void EraseObject()
        {
            if (flag)
            {
                variant = null;
                flag = false;
            }
            PrintMenu();
        }
        private void PrintAnswer()
        {
            Console.WriteLine("Введите задание");
            string answer = Console.ReadLine();
            if (answer.Length < 1)
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (answer)
                {
                    case "1": Answer1(); break;
                    case "2": Answer2(); break;
                    case "3": Answer3(); break;
                    case "4": Answer4(); break;
                    case "5": Answer5(); break;
                    case "6": Answer6(); break;
                    case "7": Answer7(); break;
                    case "8": Answer8(); break;
                    case "9": Answer9(); break;
                    case "10": Answer10(); break;
                    case "11": Answer11(); break;
                    case "12": Answer12(); break;
                    case "13": Answer13(); break;
                    case "14": Answer14(); break;
                    case "15": Answer15(); break;
                    case "16": Answer16(); break;
                    case "17": Answer17(); break;
                    case "18": Answer18(); break;
                    case "19": Answer19(); break;
                    case "20": Answer20(); break;

                }

            }
        }
        private void Answer1()
        {
            if (flag)
            {
                variant = new Dictionary<double, int>();
                flag = false;
            }
            data = new double[]{ 8240, 8958, 9230, 7818, 8333, 8500, 8647, 8285, 9032, 7959, 8240, 9230, 8333 };
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            PrintAnswer();
        }
        private void Answer2()
        {
            if (flag)
            {
                variant = new Dictionary<double, int>();
                flag = false;
            }
            data = new double[]{ 2, 5, 0, 2, 5, 3, 3, 1, 2, 3, 2, 2, 3, 4, 6 };
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _deviation = terver.Deviation(_dispersion);
            Console.WriteLine($"выборочное среднее квадратическое отклонение: {_deviation}");
            double _coefficientVariation = terver.CoefficientVariation(_sampleAverege, _deviation);
            Console.WriteLine($"коэффициент вариации: {_coefficientVariation}%");
            PrintAnswer();
        }
        private void Answer3()
        {
            if (flag)
            {
                variant = new Dictionary<double, int>();
                flag = false;
            }
            data = new double[] { 31.2, 30.7, 31.3, 30.6, 30.2, 28.9, 30.3, 30.8, 32.7, 27.6, 29.5, 31.0, 32.2, 29.7, 31.3, 30.7, 33.1, 29.2, 28.2, 26.5, 28.3, 28.9, 30.7, 27.3, 27.5, 34.8, 35.3, 32.3, 28.6, 31.4, 29.0, 31.1, 28.5, 30.4, 27.2, 27.1, 29.5, 30.8, 28.2, 36.6, 31.7, 28.6, 30.0, 25.9, 25.6, 29.4, 32.5, 32.6, 25.6, 28.9, 28.2, 27.6, 30.0, 30.4, 26.9, 29.8, 31.6, 33.0, 30.7, 28.6, 28.0, 29.8, 35.2, 28.3, 29.6, 31.9, 30.7, 28.3, 32.2, 29.1, 25.4, 29.5, 28.0, 32.2, 30.9, 27.5, 30.9, 32.4, 29.7, 32.2, 30.6, 32.1, 33.2, 32.0, 32.2, 34.9, 27.6, 27.0, 28.8, 33.0, 31.0, 27.6, 30.9, 28.5, 32.1, 31.8, 32.7, 29.8, 30.4, 26.6 };
            variant = terver.Addter(data); 
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _deviation = terver.Deviation(_dispersion);
            Console.WriteLine($"выборочное среднее квадратическое отклонение: {_deviation}");
            double _coefficientVariation = terver.CoefficientVariation(_sampleAverege, _deviation);
            Console.WriteLine($"коэффициент вариации: {_coefficientVariation}%");
            PrintAnswer();
        }
        private void Answer4()
        {
            data = new double[] { 26.0, 24.2, 23.4, 23.0, 30.8, 28.7, 34.2, 29.6, 24.4, 28.5, 27.3, 33.8, 24.4, 39.9, 30.7, 32.2 };
            double _y = 0.95;
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _correctdispersion = terver.CorrecteDispersion(_dispersion);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            string _confidenceIntervalMathematicalExpectation = terver.ConfidenceIntervalMathematicalExpectation(_sampleAverege, _correctdeviation, _y);
            Console.WriteLine($"доверительный интервал для математического ожидания: {_confidenceIntervalMathematicalExpectation}");
            string _confidenceIntervalDeviationExpectation = terver.ConfidenceIntervalDeviationExpectation(_correctdispersion, _y);
            Console.WriteLine($"доверительный интервал для среднего квадратического отклонения: {_confidenceIntervalDeviationExpectation}");
            PrintAnswer();
        }
        private void Answer5()
        {
            data = new double[] { 1.4, 9.4, 9.9, 8.3, 8.7, 1.6, 2.0, 5.8, 17.7, 10.5, 5.3, 7.8, -0.2, 8.3, 12.2, 1.3, 2.7, 0.2, 6.7, 12.1, 6.6, 5.3, 9.8, 9.1, 4.8, 5.8, 0.1, 7.6, 10.6, 7.7, 7.7, 6.7, -0.6, -0.6, 7.5, 13.2, 7.2, 8.1, -0.7, 6.2, 10.3, 8.0, 2.6, 9.8, 1.8, 2.4, 7.5, 7.4, 9.3, 3.3, 7.4, -2.9, -3.5, 0.4, 1.5, 3.1, 9.6, 8.8, 3.9, 6.0, 9.0, 3.0, 10.9, -1.9, 3.6, 11.5, 6.9, 6.6, 2.7, 5.8, 4.4, 2.2, -2.7, 15.7, 1.6, 5.2, 2.0, 11.3, 6.4, 2.8, 14.1, -4.1, 6.2, 8.2, 3.3, 11.3, 1.0, 7.7, 13.0, 10.0, -1.3, 0.8, 4.5, 8.2, 1.8, 2.3, -2.0, 9.9, 2.3, 6.4};
            double _y = 0.99, _correctdeviation = 5;
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            string _confidenceIntervalMathematicalExpectation = terver.ConfidenceIntervalMathematicalExpectation(_sampleAverege, _correctdeviation, _y);
            Console.WriteLine($"доверительный интервал для математического ожидания: {_confidenceIntervalMathematicalExpectation}");
            PrintAnswer();
        }
        private void Answer6()
        {
            data = new double[] { 30.6, 32.1, 33.2, 32.0, 32.2, 34.9, 27.6, 27.0, 28.8, 33.0, 31.0, 27.6, 30.9, 28.5, 32.1, 31.8};
            double _y = 0.95;
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _correctdispersion = terver.CorrecteDispersion(_sampleAverege);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            string _confidenceIntervalMathematicalExpectation = terver.ConfidenceIntervalMathematicalExpectation(_sampleAverege, _correctdeviation, _y);
            Console.WriteLine($"доверительный интервал для математического ожидания: {_confidenceIntervalMathematicalExpectation}");
            PrintAnswer();
        }
        private void Answer7()
        {
            data = new double[] { 65, 55, 78, 70, 55, 56, 70, 75, 57, 56, 58, 56, 54, 52, 54, 73, 67, 73, 53, 60, 62, 60, 63, 63, 62, 59, 55, 53, 55, 78, 53, 78, 50, 61, 75, 60, 68, 80, 62, 52, 75, 66, 50, 73, 67, 65, 60, 44, 61, 60, 70, 45, 55, 53, 47, 65, 80, 55, 64, 45, 65, 75, 78, 80, 70 };
            double _a = 0.05;
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            Dictionary<double, int> variant1 = new Dictionary<double, int>() { { 47, 5 }, { 53, 16}, { 59, 13}, { 65, 12}, { 71, 8}, { 77, 11} };
            terver.AddDictonary(variant1);
            _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _correctdispersion = terver.CorrecteDispersion(_dispersion);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            double[] np = new double[variant1.Count];
            int i = 0;
            foreach ( var varKeys in variant1 )
            {
                np[i] = varKeys.Key;
                i++;
            }
            double[] npOth = new double[6] { 5.6485, 10.0815, 15.4765, 15.9655, 10.9655, 4.9985};
            i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            terver.ChiSquareObservable(np, npOth, _a, 3);
            PrintAnswer();
            
        }
        private void Answer8()
        {
            double _a = 0.05;
            Dictionary<double, int> variant1 = new Dictionary<double, int>() { { 9.5, 10}, { 11.7, 18}, {13.9, 50}, { 16.1, 77}, { 18.3, 76}, { 20.5, 68}, { 22.7, 55}, { 24.9, 12}, { 27.1, 4}, { 29.3, 3} };
            terver.AddDictonary(variant1);
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _correctdispersion = terver.CorrecteDispersion(_dispersion);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            double[] np = new double[variant1.Count];
            int i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Key;
                i++;
            }
            double[] npOth = new double[10] { 9.1012, 21.5967, 46.1774, 74.2643, 82.1719, 71.0192, 42.2609, 18.6873, 6.0799, 1.3801};
            i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            terver.ChiSquareObservable(np, npOth, _a, 3);
            PrintAnswer();
        }
        private void Answer9()
        {
            double _a = 0.05;
            Dictionary<double, int> variant1 = new Dictionary<double, int>() { { 1.1, 183 }, { 3.3, 93 }, { 5.5, 27 }, { 7.7, 22 }, { 9.9, 8 }, { 12.1, 9 }, { 14.3, 4 }, { 16.5, 1 }, { 18.7, 1 }, { 20.9, 3 } };
            terver.AddDictonary(variant1);
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _correctdispersion = terver.CorrecteDispersion(_dispersion);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            double[] np = new double[variant1.Count + 1];
            int i = 1;
            np[0] = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Key + 1.1;
                i++;
            }
            double[] npOth = terver.ChanchePokaz(np, 1 / _sampleAverege);
            i = 0;
            np = new double[variant1.Count];
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            terver.ChiSquareObservable(np, npOth, _a, 2);
            PrintAnswer();
        }
        private void Answer10()
        {
            double _a = 0.01;
            Dictionary<double, int> variant1 = new Dictionary<double, int>() { { 0.2, 37 }, { 0.4, 31 }, { 0.6, 49 }, { 0.8, 34 }, { 1, 50 }, { 1.2, 36 }, { 1.4, 34 }, { 1.6, 49 }, { 1.8, 29 }, { 2, 30 } };
            terver.AddDictonary(variant1);
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _correctdispersion = terver.CorrecteDispersion(_dispersion);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            double[] np = new double[variant1.Count];
            int i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Key + 0.1;
                i++;
            }
            double[] npOth = terver.ChancheRavnom(np, (_sampleAverege - (Math.Sqrt(3) * _correctdeviation)), (_sampleAverege + (Math.Sqrt(3) * _correctdeviation)));
            i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            terver.ChiSquareObservable(np, npOth, _a, 3);
            PrintAnswer();
        }
        private void Answer11()
        {
            data = new double[] { 9.6, 7.1, 3.6, 11.2, 7.1, 13.6, 18.3, 9.4, 13.8, 3.0, 1.8, 8.3, -4.5, 4.8, -5.4, 10.6, 14.5, 2.8, -3.7, -0.6, 7.6, 4.4, 14.3, 6.4, 4.6, 13.6, 5.1, 7.4, 1.3, 3.8, 3.2, 13.5, 7.6, 7.1, 8.9, -2.7, 3.6, 9.8, -3.4, 8.4, 6.2, 0.8, 7.5, -0.8, 6.8, 8.0, 3.4, 7.3, 0.7, 9.0, 11.7, 5.4, 9.6, 10.6, 0.8, 6.1, 6.9, 5.7, 12.1, 3.3, 7.8, 3.0, 5.8, 1.6, 0.6, 1.7, 11.1, 11.0, 0.3, -3.4, 5.2, 7.7, 4.2, 1.4, 4.7, 2.1, -2.2, 7.4, 14.9, 7.6, 4.3, 10.1, 9.2, 3.4, 10.6, 4.8, 0.4, 7.5, 7.8, 10.3, 11.1, 5.2, 8.2, 11.0, 11.9, 13.0, 8.7, 4.9, 8.3, -2.1 };
            double _a = 0.025;
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            Dictionary<double, int> variant1 = new Dictionary<double, int>() { {-3.63, 8 }, {-0.23, 10 }, {3.17, 21 }, {6.57, 28 }, {9.97, 21 }, {13.37, 11 }, {16.77, 1 } };
            terver.AddDictonary(variant1);
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _correctdispersion = terver.CorrecteDispersion(_dispersion);
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");


            Console.WriteLine("\n\n\nПоказательное распределение");
            double[] np = new double[variant1.Count + 1];
            int i = 1;
            np[0] = -5.33;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Key + 1.7;
                i++;
            }
            double[] npOth = terver.ChanchePokaz(np, 1 / _sampleAverege);
            double[] npoth1 = new double[variant1.Count - 1];
            for (i = 0; i < npoth1.Length; i++)
                npoth1[i] = npOth[i];
            npoth1[npoth1.Length - 1] += npOth[npOth.Length - 1];
            i = 0;
            np = new double[variant1.Count];
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            double[] np1 = new double[variant1.Count - 1];
            for (i = 0; i < np1.Length; i++)
                np1[i] = np[i];
            np1[np1.Length - 1] += np[np.Length - 1];
            terver.ChiSquareObservable(np1, npoth1, _a, 2);


            Console.WriteLine("\n\n\nРавномерное распределение");
            np = new double[variant1.Count];
            i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Key + 1.7;
                i++;
            }
            npOth = terver.ChancheRavnom(np, (_sampleAverege - (Math.Sqrt(3) * _correctdeviation)), (_sampleAverege + (Math.Sqrt(3) * _correctdeviation)));
            i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            terver.ChiSquareObservable(np, npOth, _a, 3);


            Console.WriteLine("\n\n\nНормальное распределение");
            np = new double[variant1.Count];
            npOth = new double[7] { 2.26, 15.62, 23.8, 26.76, 19.86, 8.76, 2.45 };
            i = 0;
            foreach (var varKeys in variant1)
            {
                np[i] = varKeys.Value;
                i++;
            }
            terver.ChiSquareObservable(np, npOth, _a, 3);
            PrintAnswer();
        }
        private void Answer12()
        {
            double _a = 0.1;
            data = new double[] { 55.2, 53.6, 54.6, 57.8, 53.3, 51.8, 51.9, 50.6, 49.7, 59.2, 56.3, 53.6 };
            variant = terver.Addter(data);
            /*
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            */
            double _sampleAverege1 = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege1}");
            double _dispersion1 = terver.Dispersion(_sampleAverege1);
            Console.WriteLine($"выборочная дисперсия: {_dispersion1}");
            data = new double[] { 34.7, 29.7, 29.4, 28.8, 21.8, 35.8, 30.3, 39.5, 21.1, 39.4, 33.2, 33.3 };
            variant = terver.Addter(data);
            /*
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            */
            double _sampleAverege2 = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege2}");
            double _dispersion2 = terver.Dispersion(_sampleAverege2);
            Console.WriteLine($"выборочная дисперсия: {_dispersion2}");
            PrintAnswer();
        }
        private void Answer13()
        {
            double _a = 0.1;
            data = new double[] { 171.3, 46.1, 119.5, 16.9, 66.6, 52.0, 83.2, 77.7, 41.9, 35.4 };
            variant = terver.Addter(data);
            /*
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            */
            double _sampleAverege1 = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege1}");
            double _dispersion1 = terver.Dispersion(_sampleAverege1);
            Console.WriteLine($"выборочная дисперсия: {_dispersion1}");
            double _deviation1 = terver.Deviation(_dispersion1);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_deviation1}");
            data = new double[] { 61.0, 34.7, 62.8, 59.4, 39.2, 37.2, 21.6, 75.8, 42.1, 45.2, 73.1, 20.8, 92.0, 65.2, 83.1, 45.2, 116.9 };
            variant = terver.Addter(data);
            /*
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            */
            double _sampleAverege2 = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege2}");
            double _dispersion2 = terver.Dispersion(_sampleAverege2);
            Console.WriteLine($"выборочная дисперсия: {_dispersion2}");
            double _deviation2 = terver.Deviation(_dispersion2);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_deviation2}");
            PrintAnswer();
        }
        private void Answer14()
        {
            double _a = 0.1;
            data = new double[] { 43.0, 81.3, 52.6, 50.1, -11.3, 18.5, 19.5, -2.3, 7.3, 23.3, 61.5, 20.4, 17.2, 42.9, 1.1, 18.5, 55.3, 41.6, 42.4, 48.0, 13.3, 28.1, 16.6, 39.4, 13.6, 65.6, 40.7, 24.9, 45.7, 33.0, 62.4, 73.7, -23.5, 50.8, 70.0, 36.2, 18.0, 34.6, 52.9, -4.8, 64.5, 21.0, 53.7, 35.2, 68.4, 53.4, 70.4, 61.6, 50.8, 34.6, 33.6, 18.6, -12.7, 53.1, 16.1, 24.9, 34.2, 52.2, 27.8, 26.1, 49.0, 65.4, 66.9, 54.6, 15.5, 27.1, 22.8, 40.6, 55.4, 48.9, 10.5, 22.1, 71.7, 72.6, 15.1, 30.3, 42.8, 13.6, -14.6, 50.2, 48.6, 45.6, 10.8, 22.9, 57.2, 29.1, 15.3, 21.9, 34.7, 45.6, 92.7, 14.4, 30.1, 17.1, 17.4, 4.6, 78.4, 49.1, 37.2 };
            variant = terver.Addter(data);
            /*
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            */
            double _sampleAverege1 = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege1}");
            double _dispersion1 = terver.Dispersion(_sampleAverege1);
            Console.WriteLine($"выборочная дисперсия: {_dispersion1}");
            double _deviation1 = terver.Deviation(_dispersion1);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_deviation1}");
            data = new double[] { -10.1, 54.3, -24.6, 0.9, 21.8, 81.9, -32.8, -9.4, -27.7, 59.7, 75.3, 77.8, 13.4, 34.8, -54.0, -1.0, 26.7, 31.7, 51.9, 3.5, 39.4, 109.8, 46.4, -35.1, 24.7, 1.8, -3.1, 46.5, 13.2, 62.8, 36.9, -11.0, -24.7, 17.4, 18.0, 45.5, 39.8, -10.4, -27.5, -14.4, 25.2, 74.3, -17.9, 37.4, 29.3, 86.3, 33.6, 80.7, 35.9, 15.6, -5.3, 34.9, -0.7, 5.9, 50.2, 65.4, 85.2, -0.4, 145.1, -10.5, 104.6, 85.1, 7.2, -9.4, 92.5, 56.5, 23.5, 146.7, 28.6, -51.8, 13.8, 33.7, -17.8, 16.6, 71.5, 25.6, 61.6, 54.9, -13.7, -26.3, 52.8, 31.1, 44.5, 48.0, 3.7, 78.0, 32.7, 37.8, 51.6, 10.3, 78.7, 57.5, 89.4, 70.9, 6.6, 64.0, 52.4, 56.3, 57.8 };
            variant = terver.Addter(data);
            /*
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            */
            double _sampleAverege2 = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege2}");
            double _dispersion2 = terver.Dispersion(_sampleAverege2);
            Console.WriteLine($"выборочная дисперсия: {_dispersion2}");
            double _deviation2 = terver.Deviation(_dispersion2);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_deviation2}");
            PrintAnswer();
        }
        private void Answer16()
        {
            double a = 0.05;
            double[] dataX1 = { 21.73, 28.47, 23.46, 30.60, 27.14, 29.83, 29.53, 27.56, 28.65, 27.56 };
            double[] dataX2 = { 28.70, 25.14, 26.55, 22.64, 29.91, 31.70, 27.90, 30.74, 30.39, 25.35 };
            double[] dataX = terver.SampleVariant(dataX1, dataX2);
            variant = terver.Addter(dataX);
            terver.Print();
            double _sampleAveregeX = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее X: {_sampleAveregeX}");
            double _dispersionX = terver.Dispersion(_sampleAveregeX) * (10 / 9);
            Console.WriteLine($"исправленная выборочная дисперсия X: {_dispersionX}");
            double _deviationX = terver.Deviation(_dispersionX);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение X: {_deviationX}");
            double[] dataY1 = { 28.02, 26.79, 23.58, 27.96, 29.02, 26.78, 28.16, 25.91, 31.84, 29.73 };
            double[] dataY2 = { 27.36, 25.56, 25.72, 25.56, 26.97, 34.73, 30.41, 32.09, 29.01, 28.44 };
            double[] dataY = terver.SampleVariant(dataY1, dataY2);
            variant = terver.Addter(dataY);
            terver.Print();
            double _sampleAveregeY = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее Y: {_sampleAveregeY}");
            double _dispersionY = terver.Dispersion(_sampleAveregeY) * (10 / 9);
            Console.WriteLine($"исправленная выборочная дисперсия Y: {_dispersionY}");
            double _deviationY = terver.Deviation(_dispersionY);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение Y: {_deviationY}");
            //double[] dataXY = terver.SampleVariant(dataX, dataY);
            //variant = terver.Addter(dataXY);
            double _sampleAveregeXY = terver.SampleAverage(dataX, dataY);
            Console.WriteLine(_sampleAveregeXY);
            double r = (_sampleAveregeXY - _sampleAveregeX * _sampleAveregeY) / (_dispersionX * _dispersionY);
            Console.WriteLine($"Коэффициент корреляции: {r}");
            Console.WriteLine($"t -критерий Стьюдента: {Math.Abs(r) * Math.Sqrt(8 / (1- Math.Pow(r, 2)))}");
            Console.WriteLine($"\nвыборочная дисперсия X: {_dispersionX * 0.9}\tвыборочная дисперсия Y: {_dispersionY * 0.9}\nвыборочное среднее квадратическое отклонение X: {Math.Sqrt(_dispersionX * 0.9)}\tвыборочное среднее квадратическое отклонение Y: {Math.Sqrt(_dispersionY * 0.9)}");
            PrintAnswer();
        }
        private void Answer15()
        {

        }
        private void Answer17()
        {


        }
        private void Answer18()
        {

        }
        private void Answer19()
        {

        }
        private void Answer20()
        {
            if (flag)
            {
                variant = new Dictionary<double, int>();
                flag = false;
            }
            data = new double[] { 42, 21, 31, 18, 27, 28, 48, 26, 28, 62, 56, 26, 18, 22, 26, 52, 16, 53, 20, 46, 48, 81, 38, 88, 86, 58, 49, 53, 18, 22, 60, 44, 60, 44, 71 };
            variant = terver.Addter(data);
            Console.WriteLine("Cтатистическое распределение выборки");
            terver.Print();
            Console.WriteLine("\nинтервальный ряд распределения относительных частот");
            terver.PrintPeriodicity();
            double _sampleAverege = terver.SampleAverage();
            Console.WriteLine($"выборочное среднее: {_sampleAverege}");
            double _dispersion = terver.Dispersion(_sampleAverege);
            Console.WriteLine($"выборочная дисперсия: {_dispersion}");
            double _deviation = terver.Deviation(_dispersion);
            Console.WriteLine($"выборочное среднее квадратическое отклонение: {_deviation}");
            double _coefficientVariation = terver.CoefficientVariation(_sampleAverege, _deviation);
            Console.WriteLine($"коэффициент вариации: {_coefficientVariation}%");
            double _y = 0.95;
            double _correctdispersion = _dispersion * (data.Length / (data.Length - 1));
            Console.WriteLine($"испправленная выборочная дисперсия: {_correctdispersion}");
            double _correctdeviation = terver.CorrecteDeviation(_correctdispersion);
            Console.WriteLine($"исправленное выборочное среднее квадратическое отклонение: {_correctdeviation}");
            string _confidenceIntervalMathematicalExpectation = terver.ConfidenceIntervalMathematicalExpectation(_sampleAverege, _correctdeviation, _y);
            Console.WriteLine($"доверительный интервал для математического ожидания: {_confidenceIntervalMathematicalExpectation}");
            string _confidenceIntervalDeviationExpectation = terver.ConfidenceIntervalDeviationExpectation(_correctdeviation, _y);
            Console.WriteLine($"доверительный интервал для среднего квадратического отклонения: {_confidenceIntervalDeviationExpectation}");
            PrintAnswer();
        }
        public void PrintMenu()
        {
            Console.WriteLine("1.   Создать объект класса\n2.   Вывести объект класса\n3.   Стереть объект\n4.   Решения задач");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "1" || line != "2" || line != "3" || line != "4"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "1": CreateObject(); break;
                    case "2": PrintObject(); break;
                    case "3": EraseObject(); break;
                    case "4": PrintAnswer(); break;
                }
            }
        }
    }
}
