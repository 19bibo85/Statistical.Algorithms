using Statistical.Algorithms.Interfaces;
using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;
using System;

namespace Statistical.Algorithms.Regressions
{
    public abstract class BaseRegressionStrategy : IRegressionStrategy
    {
        protected readonly object _lock = new object();

        private RegressionModel? _model;

        public RegressionModel? Model
        {
            get
            {
                lock (_lock)
                {
                    return _model;
                }
            }
            protected set
            {
                lock (_lock)
                {
                    _model = value;
                }
            }
        }

        public abstract void Fit(DataPoint[] data);

        public double[] Predict(double[] xValues)
        {
            if (Model == null)
                throw new NullReferenceException("You must Fit data first.");

            var yValues = new double[xValues.Length];
            for (int i=0; i < xValues.Length; i++) 
            {
                double yValue = 0;
                for (int j=0; j < Model.Coefficients.Length; j++) 
                {
                    yValue += Math.Pow(xValues[i], j) * Model.Coefficients[j];
                }

                yValues[i] = yValue;
            }
            return yValues;
        }

        protected double[] GaussianElimination(double[,] A, double[] b)
        {
            int n = b.Length;

            // Forward elimination
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    double factor = A[j, i] / A[i, i];
                    for (int k = i; k < n; k++)
                    {
                        A[j, k] -= factor * A[i, k];
                    }
                    b[j] -= factor * b[i];
                }
            }

            // Back substitution
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += A[i, j] * x[j];
                }
                x[i] = (b[i] - sum) / A[i, i];
            }

            return x;
        }
    }
}
