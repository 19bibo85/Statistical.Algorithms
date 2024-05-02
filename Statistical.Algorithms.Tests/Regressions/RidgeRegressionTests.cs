using Statistical.Algorithms.Regressions;
using Statistical.Algorithms.Structs;
using Xunit;

namespace Statistical.Algorithms.Tests.Regressions
{
    public class RidgeRegressionTests
    {
        #region Private Members

        private readonly RidgeRegressionStrategy RidgeRegression;

        #endregion

        #region Constructor

        public RidgeRegressionTests()
        {
            RidgeRegression = new RidgeRegressionStrategy(lambda: 0.0);
        }

        #endregion

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_When_fitting_a_ridge_regression_Then_return_1st_grade_equation_with_no_errors()
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
            RidgeRegression.Lambda = 0.0;
            RidgeRegression.Fit(data);

            // ASSERT
            Assert.NotNull(RidgeRegression.Model);
            Assert.NotEmpty(RidgeRegression.Model.Coefficients);
            Assert.Equal(2, RidgeRegression.Model.Coefficients.Length);
            Assert.Equal(0, RidgeRegression.Model.Coefficients[0]);
            Assert.Equal(2, RidgeRegression.Model.Coefficients[1]);
            Assert.Equal(0, RidgeRegression.Model.MAE);
            Assert.Equal(0, RidgeRegression.Model.MSE);
            Assert.Equal(0, RidgeRegression.Model.RMSE);
        }

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_and_two_outlier_When_fitting_a_ridge_regression_Then_return_the_best_1st_grade_equation_with_errors()
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 2 },
                new() { X = 2, Y = 4 },
                new() { X = 3, Y = 3 },
                new() { X = 4, Y = 8 },
                new() { X = 5, Y = 15 }
            };

            // ACT
            RidgeRegression.Lambda = 1.0;
            RidgeRegression.Fit(data);

            // ASSERT
            Assert.NotNull(RidgeRegression.Model);
            Assert.NotEmpty(RidgeRegression.Model.Coefficients);
            Assert.Equal(2, RidgeRegression.Model.Coefficients.Length);
            Assert.NotEqual(0, RidgeRegression.Model.MAE);
            Assert.NotEqual(0, RidgeRegression.Model.MSE);
            Assert.NotEqual(0, RidgeRegression.Model.RMSE);
        }

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_2x_When_predicting_values_using_ridge_regression_Then_return_the_predicted_values_with_no_errors()
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
            RidgeRegression.Lambda = 0.0;
            RidgeRegression.Fit(data);

            var yValues = RidgeRegression.Predict(xValues);

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
