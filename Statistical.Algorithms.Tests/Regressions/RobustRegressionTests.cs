using Statistical.Algorithms.Regressions;
using Statistical.Algorithms.Structs;
using Xunit;

namespace Statistical.Algorithms.Tests.Regressions
{
    public class RobustRegressionTests
    {
        #region Private Members

        private readonly RobustRegressionStrategy RobustRegression;

        #endregion

        #region Constructor

        public RobustRegressionTests()
        {
            RobustRegression = new RobustRegressionStrategy(minInliers: 1);
        }

        #endregion

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_When_fitting_a_robust_regression_Then_return_1st_grade_equation_with_no_errors()
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 2 },
                new() { X = 2, Y = 4 },
                new() { X = 3, Y = 6 },
                new() { X = 4, Y = 8 },
                new() { X = 5, Y = 10 }
            };

            // ACT
            RobustRegression.Fit(data);

            // ASSERT
            Assert.NotNull(RobustRegression.Model);
            Assert.NotEmpty(RobustRegression.Model.Coefficients);
            Assert.Equal(2, RobustRegression.Model.Coefficients.Length);
            Assert.Equal(0, RobustRegression.Model.Coefficients[0]);
            Assert.Equal(2, RobustRegression.Model.Coefficients[1]);
            Assert.Equal(0, RobustRegression.Model.MAE);
            Assert.Equal(0, RobustRegression.Model.MSE);
            Assert.Equal(0, RobustRegression.Model.RMSE);
        }

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_and_one_outlier_When_fitting_a_robust_regression_Then_return_the_best_1st_grade_equation_with_errors()
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 2 },
                new() { X = 2, Y = 4 },
                new() { X = 3, Y = 6 },
                new() { X = 4, Y = 8 },
                new() { X = 5, Y = 15 }
            };

            // ACT
            RobustRegression.Fit(data);

            // ASSERT
            Assert.NotNull(RobustRegression.Model);
            Assert.NotEmpty(RobustRegression.Model.Coefficients);
            Assert.Equal(2, RobustRegression.Model.Coefficients.Length);
            Assert.NotEqual(0, RobustRegression.Model.MAE);
            Assert.NotEqual(0, RobustRegression.Model.MSE);
            Assert.NotEqual(0, RobustRegression.Model.RMSE);
        }

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_When_predicting_values_using_robust_regression_Then_return_the_predicted_values_with_no_errors() 
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 2 },
                new() { X = 2, Y = 4 },
                new() { X = 3, Y = 6 },
                new() { X = 4, Y = 8 },
                new() { X = 5, Y = 10 }
            };

            var xValues = new double[5] { 2, 4, 6, 8, 10 };

            // ACT
            RobustRegression.Fit(data);

            var yValues = RobustRegression.Predict(xValues);

            // ASSERT
            Assert.NotNull(yValues);
            Assert.NotEmpty(yValues);
            Assert.Equal(4, yValues[0]);
            Assert.Equal(8, yValues[1]);
            Assert.Equal(12, yValues[2]);
            Assert.Equal(16, yValues[3]);
            Assert.Equal(20, yValues[4]);
        }
    }
}
