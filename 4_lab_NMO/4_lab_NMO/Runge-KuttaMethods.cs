using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_lab_NMO
{
    internal class Runge_KuttaMethods
    {
        private double Funchion(double x, double y) => x + y;
        double[] _x, _y;
        double _h;
        public Runge_KuttaMethods(double a, double b, double x0, double y0, double h)
        {
            double c = (b - a) / h;
            _x = new double[Int32.Parse(c.ToString())];
            _y = new double[Int32.Parse(c.ToString())];
            _x[0] = x0;
            _y[0] = y0;
            _h = h;
            for (int i = 1; i < c; i++)
            {
                _x[i] = _h * i;
            }
        }
        public double[] SecondOrderMethod(double a)
        {
            for(int i = 0;i < _x.Length - 1;i++)
            {
                _y[i + 1] = _y[i] + _h * ((1 - a) * Funchion(_x[i], _y[i]) + a * (Funchion(_x[i] + (_h / (2 * a)), _y[i] + Funchion(_x[i], _y[i]) * (_h / (2 * a)))));
            }
            return _y;
        }
        public double[] Runge_KuttaThirdMethod1()
        {
            double k1, k2, k3;
            for(int i = 0;i < _x.Length - 1;i++)
            {
                k1 = Funchion(_x[i], _y[i]);
                k2 = Funchion(_x[i] + (_h / 2), _y[i] + (_h / 2) * k1);
                k3 = Funchion(_x[i] + _h, _y[i] - (_h * k1) + (2 * _h * k2));
                _y[i + 1] = _y[i] + (_h / 6) * (k1 + 4 * k2 + k3);
            }
            return _y;
        }
        public double[] Runge_KuttaThirdMethod2()
        {
            double k1, k2, k3;
            for (int i = 0; i < _x.Length - 1; i++)
            {
                k1 = Funchion(_x[i], _y[i]);
                k2 = Funchion(_x[i] + (_h / 3), _y[i] + (_h / 3) * k1);
                k3 = Funchion(_x[i] + (2 * _h / 3), _y[i] + (2 * _h / 3) * k2);
                _y[i + 1] = _y[i] + (_h / 4) * (k1 + 3 * k3);
            }
            return _y;
        }
        public double[] Runge_KuttamethodFourth1()
        {
            double k1, k2, k3, k4;
            for (int i = 0; i < _x.Length - 1; i++)
            {
                k1 = Funchion(_x[i], _y[i]);
                k2 = Funchion(_x[i] + (_h / 2), _y[i] + (_h / 2) * k1);
                k3 = Funchion(_x[i] + (_h / 2), _y[i] + (_h / 2) * k2);
                k4 = Funchion(_x[i] + _h, _y[i] + _h * k1);
                _y[i + 1] = _y[i] + (_h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
            }
            return _y;
        }
        public double[] Runge_KuttamethodFourth2()
        {
            double k1, k2, k3, k4;
            for (int i = 0; i < _x.Length - 1; i++)
            {
                k1 = Funchion(_x[i], _y[i]);
                k2 = Funchion(_x[i] + (_h / 4), _y[i] + (_h / 4) * k1);
                k3 = Funchion(_x[i] + (_h / 2), _y[i] + (_h / 2) * k2);
                k4 = Funchion(_x[i] + _h, _y[i] + _h * k1 - 2 * _h * k2 + 2 * _h * k3);
                _y[i + 1] = _y[i] + (_h / 6) * (k1 + 4 * k3 + k4);
            }
            return _y;
        }
    }
}
