using System;

namespace Statistical.Algorithms.Distributions
{
    public class Distribution
    {
        public static double NormalProbability(double x, double mean, double stdDev)
        {
            double coefficient = 1.0 / (stdDev * Math.Sqrt(2 * Math.PI));
            double exponent = -0.5 * Math.Pow((x - mean) / stdDev, 2);
            return coefficient * Math.Exp(exponent);
        }

        public static double PoissonProbability(double k, double lambda)
        {
            return Math.Pow(lambda, k) * Math.Exp(-lambda) / Factorial(k);
        }

        private static double Factorial(double n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }        
    }
}
