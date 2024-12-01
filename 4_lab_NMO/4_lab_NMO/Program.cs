using _4_lab_NMO;
/*
EulerMethod eulerMethod = new EulerMethod(0, 1, 0, 1, 0.1);
double[] result0 = eulerMethod.LeftTrianglesMethod();
for (int i = 0; i < result0.Length; i++)
    Console.WriteLine(result0[i]);
double[] result1 = eulerMethod.RightTrianglesMethod();
for (int i = 0; i < result1.Length; i++)
    Console.WriteLine(result0[i]);
double[] result2 = eulerMethod.MiddlePointsMethod();
for (int i = 0; i < result2.Length; i++)
    Console.WriteLine(result0[i]);
double[] result3 = eulerMethod.RecalculationMethod();
for (int i = 0; i < result3.Length; i++)
    Console.WriteLine(result0[i]);
double[] result4 = eulerMethod.RefinedMethod();
for (int i = 0; i < result4.Length; i++)
    Console.WriteLine(result0[i]);
*/
/*
Runge_KuttaMethods runge_KuttaMethods = new Runge_KuttaMethods(0, 1, 0, 1, 0.1);
double[] result0 = runge_KuttaMethods.SecondOrderMethod(2);
for (int i = 0; i < result0.Length; i++)
    Console.WriteLine(result0[i]);
double[] result1 = runge_KuttaMethods.Runge_KuttaThirdMethod1();
for (int i = 0; i < result1.Length; i++)
    Console.WriteLine(result1[i]);
double[] result2 = runge_KuttaMethods.Runge_KuttaThirdMethod2();
for (int i = 0; i < result1.Length; i++)
    Console.WriteLine(result1[i]);
double[] result3 = runge_KuttaMethods.Runge_KuttamethodFourth1();
for (int i = 0; i < result1.Length; i++)
    Console.WriteLine(result1[i]);
double[] result4 = runge_KuttaMethods.Runge_KuttamethodFourth2();
for (int i = 0; i < result1.Length; i++)
    Console.WriteLine(result1[i]);
*/
MultiStepMethods multiStepMethods = new MultiStepMethods(0, 1, 0, 1, 0.1);
double[] result0 = multiStepMethods.AdamsMethod();
for (int i = 0; i < result0.Length; i++)
    Console.WriteLine(result0[i]);
double[] result1 = multiStepMethods.MilnaMethod();
for (int i = 0; i < result1.Length; i++)
    Console.WriteLine(result1[i]);