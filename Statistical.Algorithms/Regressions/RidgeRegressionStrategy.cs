using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;

namespace Statistical.Algorithms.Regressions
{
    public class RidgeRegressionStrategy : BaseRegressionStrategy
    {
        #region Private Members

        private double _lambda;

        #endregion

        #region Constructor

        public RidgeRegressionStrategy(double lambda = 0.0)
        {
            _lambda = lambda;
        }

        #endregion

        #region Setters

        public double Lambda
        {
            set
            {
                lock (_lock)
                {
                    _lambda = value;
                }
            }
        }

        #endregion

        public override void Fit(DataPoint[] data)
        {
            int n = data.Length;

            // Construct the design matrix X and response vector y
            int p = 2; // number of features
            double[,] X = new double[n, p];
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                X[i, 0] = 1; // Bias term
                X[i, 1] = data[i].X;
                y[i] = data[i].Y;
            }

            // Adding regularization term to X^T*X
            double[,] XtX = new double[p, p];
            double[] XtY = new double[p];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    XtY[j] += X[i, j] * y[i];
                    for (int k = 0; k < p; k++)
                    {
                        XtX[j, k] += X[i, j] * X[i, k];
                    }
                }
            }
            for (int i = 0; i < p; i++)
            {
                XtX[i, i] += _lambda; // Add lambda to diagonal elements
            }

            // Solve (X^T*X)*beta = X^T*y for beta using Gaussian elimination
            var coefficients = GaussianElimination(XtX, XtY);

            Model = new RegressionModel(data, coefficients);
        }
    }
}
