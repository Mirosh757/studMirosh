using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _4_lab_NMO
{
    internal class EulerMethod
    {
        private double Funchion(double x, double y) => x + y;
        private double FormulaForMiddleTriangles(double x, double y, double h) => y + (h / 2) * Funchion(x, y);
        private double FormulaForRecalculationMethod(double x, double y, double h) => y + h * Funchion(x, y);
        double[] _x, _y;
        double _h;
        public EulerMethod(double a, double b, double x0, double y0, double h)
        {
            double c = (b - a) / h;
            _x = new double[Int32.Parse(c.ToString())]; 
            _y = new double[Int32.Parse(c.ToString())];
            _x[0] = x0;
            _y[0] = y0;
            _h = h;
            for(int i = 1;i < c;i++)
            {
                _x[i] = _h * i;
            }
        }
        public double[] LeftTrianglesMethod()
        {
            for(int i = 0;i < _x.Length - 1;i++)
            {
                _y[i + 1] = _y[i] + _h * Funchion(_x[i], _y[i]);
            }
            return _y;
        }
        public double[] RightTrianglesMethod()
        {
            _y[1] = _y[0] + _h * Funchion(_x[0], _y[0]);
            for (int i = 1; i < _x.Length - 1; i++)
            {
                _y[i + 1] = _y[i] + _h * Funchion(_x[i], _y[i]);
            }
            return _y;
        }
        public double[] MiddlePointsMethod()
        {
            _y[1] = _y[0] + _h * Funchion(_x[0], _y[0]);
            for (int i = 0; i < _x.Length - 1; i++)
            {
                _y[i + 1] = _y[i] + _h * Funchion(_x[i] + (_h / 2), FormulaForMiddleTriangles(_x[i], _y[i], _h));
            }
            return _y;
        }
        public double[] RecalculationMethod()
        {
            for(int i = 0; i < _x.Length - 1; i++)
            {
                _y[i + 1] = _y[i] + (_h / 2) * (Funchion(_x[i], _y[i]) + Funchion(_x[i + 1], FormulaForRecalculationMethod(_x[i], _y[i], _h)));
            }
            return _y;
        }
        public double[] RefinedMethod()
        {
            _y[1] = FormulaForRecalculationMethod(_x[0], _y[0], _h);
            for (int i = 1; i < _x.Length - 2; i++)
            {
                _y[i + 1] = _y[i - 1] + 2 * _h * Funchion(_x[i], _y[i]);
            }
            return _y;
        }
    }
}
