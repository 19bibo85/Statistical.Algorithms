using Statistical.Algorithms.Distributions;
using Statistical.Algorithms.Shared;
using Xunit;

namespace Statistical.Algorithms.Tests.Distributions
{
    public class DistributionTests
    {
        [Fact]
        public void Given_x_mean_and_stddev_When_using_a_normal_distribution_Then_the_probability_is_correctly_calculated()
        {
            double[] numbers = [3, 0, 1, 0, 4, 2, 6, 2, 0, 3];
            var mean = MathHelpers.CalculateMean(numbers); // 2.1
            var stdDev = MathHelpers.CalculateStandardDeviation(numbers); // 1.87

            double c = 1.0 / 10000;
            double tot = 0;
            for (var i = mean - stdDev; i <= mean + stdDev; i += c)
                tot += Distribution.NormalProbability(i, mean, stdDev);

            var withinOneStdDev = Math.Round(tot * c * 100, 0);

            Assert.Equal(68, withinOneStdDev);
        }

        [Fact]
        public void Given_a_list_of_numbers_When_calculating_mean_Then_the_number_returned_is_correct()
        {
            double[] numbers = [3, 0, 1, 0, 4, 2, 6, 2, 0, 3];

            var result = MathHelpers.CalculateMean(numbers);

            Assert.Equal(2.1, Math.Round(result, 2));
        }

        [Fact]
        public void Given_a_list_of_numbers_When_calculating_standard_deviation_Then_the_number_returned_is_correct()
        {
            double[] numbers = [3, 0, 1, 0, 4, 2, 6, 2, 0, 3];

            var result = MathHelpers.CalculateStandardDeviation(numbers);

            Assert.Equal(1.87, Math.Round(result, 2));
        }

        [Fact]
        public void Given_x_and_mean_When_using_a_poisson_distribution_Then_the_probability_is_correctly_calculated()
        {
            var result = Distribution.PoissonProbability(0, 1.23);

            Assert.Equal(0.29, Math.Round(result, 2));
        }
    }
}
