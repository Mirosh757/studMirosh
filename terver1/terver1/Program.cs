using terver1;
Facade facade = new Facade();
facade.PrintMenu();

/*
Terver terver = new Terver();
Dictionary<double, int> variant = new Dictionary<double, int>();
variant = terver.Addter();
terver.Print();
Console.WriteLine();
terver.PrintPeriodicity();
double sampleAverage = terver.SampleAverage();
Console.WriteLine(sampleAverage);
double dis = terver.Dispersion(sampleAverage);
double dev = terver.Deviation(dis);
double cordis = terver.CorrecteDispersion(dis);
double cordev = terver.CorrecteDeviation(cordis);
Console.WriteLine($"{dis} {dev}");
Console.WriteLine(terver.ConfidenceIntervalMathematicalExpectation(sampleAverage, dev, 15, 0.95));
Console.WriteLine(terver.ConfidenceIntervalDeviationExpectation(15, dis, 0.95));
/*
double dis = terver.Dispersion(sampleAverage);
double dev = terver.Deviation(dis);
Console.WriteLine($"{dis} {dev}");
Console.WriteLine(terver.CoefficientVariation(sampleAverage, dev));
// 8240 8958 9230 7818 8333 8500 8647 8285 9032 7959 8240 9230 8333
// 2 5 0 2 5 3 3 1 2 3 2 2 3 4 6
/*
31,2 30,7 31,3 30,6 30,2 28,9 30,3 30,8 32,7 27,6 29,5 31,0 32,2 29,7 31,3 30,7 33,1 29,2 28,2 26,5 28,3 28,9 30,7 27,3 27,5 34,8 35,3 32,3 28,6 31,4 29,0 31,1 28,5 30,4 27,2 27,1 29,5 30,8 28,2 36,6 31,7 28,6 30,0 25,9 25,6 29,4 32,5 32,6 25,6 28,9 28,2 27,6 30,0 30,4 26,9 29,8 31,6 33,0 30,7 28,6 28,0 29,8 35,2 28,3 29,6 31,9 30,7 28,3 32,2 29,1 25,4 29,5 28,0 32,2 30,9 27,5 30,9 32,4 29,7 32,2 30,6 32,1 33,2 32,0 32,2 34,9 27,6 27,0 28,8 33,0 31,0 27,6 30,9 28,5 32,1 31,8 32,7 29,8 30,4 26,6

26,0 24,2 23,4 23,0 30,8 28,7 34,2 29,6 24,4 28,5 27,3 33,8 24,4 39,9 30,7 32,2
*/