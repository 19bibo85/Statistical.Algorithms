using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;

namespace Statistical.Algorithms.Regressions
{
    public class LinearRegressionStrategy : BaseRegressionStrategy
    {
        public override void Fit(DataPoint[] data)
        {
            double sumX = 0;
            double sumY = 0;
            double sumXY = 0;
            double sumXX = 0;

            int n = data.Length;

            foreach (var point in data)
            {
                sumX += point.X;
                sumY += point.Y;
                sumXY += point.X * point.Y;
                sumXX += point.X * point.X;
            }

            double slope = (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX);
            double intercept = (sumY - slope * sumX) / n;

            Model = new RegressionModel(data, new double[2] { intercept, slope });
        }
    }
}
