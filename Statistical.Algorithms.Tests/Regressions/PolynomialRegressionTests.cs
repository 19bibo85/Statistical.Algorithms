using Statistical.Algorithms.Regressions;
using Statistical.Algorithms.Structs;
using Xunit;

namespace Statistical.Algorithms.Tests.Regressions
{
    public class PolynomialRegressionTests
    {
        #region Private Members

        private readonly PolynomialRegressionStrategy PolynomialRegression;

        #endregion

        #region Constructor

        public PolynomialRegressionTests()
        {
            PolynomialRegression = new PolynomialRegressionStrategy(degree: 2);
        }

        #endregion

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_3x2_4x_m1_When_fitting_a_polynomial_regression_Then_return_2nd_grade_equation_with_no_errors()
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 6 },
                new() { X = 2, Y = 19 },
                new() { X = 3, Y = 38 },
                new() { X = 4, Y = 63 },
                new() { X = 5, Y = 94 }
            };

            // ACT
            PolynomialRegression.Fit(data);

            // ASSERT
            Assert.NotNull(PolynomialRegression.Model);
            Assert.NotEmpty(PolynomialRegression.Model.Coefficients);
            Assert.Equal(3, PolynomialRegression.Model.Coefficients.Length);
            Assert.Equal(-1, PolynomialRegression.Model.Coefficients[0]);
            Assert.Equal(4, PolynomialRegression.Model.Coefficients[1]);
            Assert.Equal(3, PolynomialRegression.Model.Coefficients[2]);
            Assert.Equal(0, PolynomialRegression.Model.MAE);
            Assert.Equal(0, PolynomialRegression.Model.MSE);
            Assert.Equal(0, PolynomialRegression.Model.RMSE);
        }

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_3x2_4x_m1_and_one_outlier_When_fitting_a_polynomial_regression_Then_return_2nd_grade_equation_with_errors()
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 6 },
                new() { X = 2, Y = 19 },
                new() { X = 3, Y = 38 },
                new() { X = 4, Y = 63 },
                new() { X = 5, Y = 120 }
            };

            // ACT
            PolynomialRegression.Fit(data);

            // ASSERT
            Assert.NotNull(PolynomialRegression.Model);
            Assert.NotEmpty(PolynomialRegression.Model.Coefficients);
            Assert.Equal(3, PolynomialRegression.Model.Coefficients.Length);            
            Assert.NotEqual(0, PolynomialRegression.Model.MAE);
            Assert.NotEqual(0, PolynomialRegression.Model.MSE);
            Assert.NotEqual(0, PolynomialRegression.Model.RMSE);
        }

        [Fact]
        public void Given_a_list_of_data_point_with_y_equal_to_3x2_4x_m1_When_predicting_values_using_polynomial__regression_Then_return_the_predicted_values_with_no_errors()
        {
            // ARRANGE
            var data = new DataPoint[5]
            {
                new() { X = 1, Y = 6 },
                new() { X = 2, Y = 19 },
                new() { X = 3, Y = 38 },
                new() { X = 4, Y = 63 },
                new() { X = 5, Y = 94 }
            };

            var xValues = new double[5] { 2, 4, 6, 8, 10 };

            // ACT
            PolynomialRegression.Fit(data);

            var yValues = PolynomialRegression.Predict(xValues);

            // ASSERT
            Assert.NotNull(yValues);
            Assert.NotEmpty(yValues);
            Assert.Equal(19, yValues[0]);
            Assert.Equal(63, yValues[1]);
            Assert.Equal(131, yValues[2]);
            Assert.Equal(223, yValues[3]);
            Assert.Equal(339, yValues[4]);
        }
    }
}
