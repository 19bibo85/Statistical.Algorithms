using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;
using System;
using System.Linq;

namespace Statistical.Algorithms.Regressions
{
    public class RobustRegressionStrategy : BaseRegressionStrategy
    {
        #region Private Member
        
        private int _minInliers;
        private double _threshold;
        private int _numIterations;

        #endregion

        #region Constructor

        public RobustRegressionStrategy(int minInliers = 0, double threshold = 0.01, int numIterations = 1000) 
        {            
            _minInliers = minInliers;
            _threshold = threshold;
            _numIterations = numIterations;
        }

        #endregion

        #region Setters

        public int MinInliers
        {
            set
            {
                lock (_lock)
                {
                    _minInliers = value;
                }
            }
        }

        public double Threshold
        {
            set
            {
                lock (_lock)
                {
                    _threshold = value;
                }
            }
        }

        public int NumIterations
        {
            set
            {
                lock (_lock)
                {
                    _numIterations = value;
                }
            }
        }

        #endregion

        public override void Fit(DataPoint[] data)
        {
            Random rand = new Random();
            double bestSlope = 0;
            double bestIntercept = 0;
            int bestInliers = 0;

            for (int i = 0; i < _numIterations; i++)
            {
                // Randomly select two points
                var sample = data.OrderBy(_ => rand.Next()).Take(2).ToList();
                var p1 = sample[0];
                var p2 = sample[1];

                // Compute line equation: y = mx + b
                double slope = (p2.Y - p1.Y) / (p2.X - p1.X);
                double intercept = p1.Y - slope * p1.X;

                // Count inliers
                int inliers = 0;
                foreach (var point in data)
                {
                    double distance = Math.Abs(point.Y - (slope * point.X + intercept));
                    if (distance < _threshold)
                        inliers++;
                }

                // Check if this model is better than the previous best model
                if (inliers > bestInliers)
                {
                    bestInliers = inliers;
                    bestSlope = slope;
                    bestIntercept = intercept;

                    // Early termination if enough inliers found
                    if (inliers >= _minInliers)
                        break;
                }
            }

            Model = new RegressionModel(data, new double[2] { bestIntercept, bestSlope });
        }
    }
}
