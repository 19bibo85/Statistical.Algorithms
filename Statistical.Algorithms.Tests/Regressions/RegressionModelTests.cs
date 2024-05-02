using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;
using Xunit;

namespace Statistical.Algorithms.Tests.Regressions
{
    public class RegressionModelTests
    {
        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_When_fitting_a_robust_regression_Then_return_1st_grade_equation_with_no_errors()
        {            
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 1 },
                new() { X = 2, Y = 2 },
                new() { X = 3, Y = 3 },
                new() { X = 4, Y = 4 },
                new() { X = 5, Y = 15 }
            };

            // ACT
            var regression = new RegressionModel(data, [0, 1]);

            // ASSERT
            Assert.NotNull(regression);
            Assert.Equal(2, regression.MAE); // (Y - X) / N => (15 - 5) / 5 = 2
            Assert.Equal(20, regression.MSE); // (Y - X) / N => (15 - 5)^2 / 5 = 20
            Assert.Equal(Math.Sqrt(regression.MSE), regression.RMSE);
        }
    }
}
