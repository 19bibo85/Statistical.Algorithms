using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;
using System;

namespace Statistical.Algorithms.Regressions
{
    public class PolynomialRegressionStrategy : BaseRegressionStrategy
    {
        #region Private Members

        private int _degree;

        #endregion

        #region Constructor

        public PolynomialRegressionStrategy(int degree = 1) 
        {
            _degree = degree;
        }

        #endregion

        #region Setters

        public int Degree
        {
            set
            {
                lock (_lock)
                {
                    _degree = value;
                }
            }
        }

        #endregion

        public override void Fit(DataPoint[] data)
        {
            int n = data.Length;
            int m = _degree + 1;

            double[,] A = new double[m, m];
            double[] b = new double[m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sumX = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sumX += Math.Pow(data[k].X, i + j);
                    }
                    A[i, j] = sumX;
                }

                double sumY = 0;
                for (int k = 0; k < n; k++)
                {
                    sumY += data[k].Y * Math.Pow(data[k].X, i);
                }
                b[i] = sumY;
            }

            var coefficients = GaussianElimination(A, b);

            Model = new RegressionModel(data, coefficients);
        }
    }
}
