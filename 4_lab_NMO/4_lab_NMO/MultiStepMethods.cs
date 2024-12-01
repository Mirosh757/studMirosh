using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_lab_NMO
{
    internal class MultiStepMethods
    {
        Runge_KuttaMethods runge_KuttaMethods;
        double[] _helpY;
        private double Funchion(double x, double y) => x + y;
        private double DeltaF1(double xLast, double yLast, double xCurrent, double yCurrent) => Funchion(xCurrent, yCurrent) - Funchion(xLast, yLast);
        private double DeltaF2(double xLastLast, double yLastLast, double xLast, double yLast, double xCurrent, double yCurrent) => Funchion(xCurrent, yCurrent) - (2 * Funchion(xLast, yLast)) + Funchion(xLastLast, yLastLast);
        double[] _x, _y;
        double _h;
        public MultiStepMethods(double a, double b, double x0, double y0, double h) 
        {
            runge_KuttaMethods = new Runge_KuttaMethods(a, b, x0, y0, h);
            double c = (b - a) / h;
            _x = new double[Int32.Parse(c.ToString())];
            _y = new double[Int32.Parse(c.ToString())];
            _x[0] = x0;
            _helpY = runge_KuttaMethods.Runge_KuttamethodFourth2();
            _y[0] = _helpY[0]; _y[1] = _helpY[1]; _y[2] = _helpY[2];
            _h = h;
            for (int i = 1; i < c; i++)
            {
                _x[i] = _h * i;
            }
        }
        public double[] AdamsMethod()
        {
            int k = 3;
            for(int i = 2;i < _x.Length - 1;i++)
            {
                _y[i + 1] = _y[i] + _h * Funchion(_x[i], _y[i]) + ((_h * _h) / 2) * DeltaF1(_x[i - 1], _y[i - 1], _x[i], _y[i]) + ((5 * _h * _h * _h) / 13) * DeltaF2(_x[i - 2], _y[i - 2], _x[i - 1], _y[i - 1], _x[i], _y[i]);
            }
            return _y;
        }
        public double[] MilnaMethod()
        {
            _y[3] = _helpY[3];
            for(int i = 4;i < _x.Length;i++)
            {
                double yPred = _y[i - 4] + (4 * _h / 3) * (2 * Funchion(_x[i - 3], _y[i - 3]) - Funchion(_x[i - 2], _y[i - 2]) + 2 * Funchion(_x[i - 1], _y[i - 1]));
                double fPred = Funchion(_x[i], yPred);
                _y[i] = _y[i - 2] + (_h / 3) * (Funchion(_x[i - 2], _y[i - 2]) + 4 * Funchion(_x[i - 1], _y[i - 1]) + fPred);
            }
            return _y;
        }
    }
}
