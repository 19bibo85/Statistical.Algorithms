using System;

namespace Statistical.Algorithms.Shared
{
    public static class MathHelpers
    {
        public static double CalculateStandardDeviation(double[] numbers)
        {
            double mean = CalculateMean(numbers);
            double sumOfSquaredDifferences = 0.0;

            foreach (double num in numbers)
            {
                sumOfSquaredDifferences += Math.Pow(num - mean, 2);
            }

            double variance = sumOfSquaredDifferences / numbers.Length;
            double standardDeviation = Math.Sqrt(variance);
            return standardDeviation;
        }

        public static double CalculateMean(double[] numbers)
        {
            double sum = 0.0;
            foreach (double num in numbers)
            {
                sum += num;
            }
            return sum / numbers.Length;
        }
    }
}
