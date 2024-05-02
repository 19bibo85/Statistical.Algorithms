using Statistical.Algorithms.Structs;
using System;

namespace Statistical.Algorithms.Models
{
    public class RegressionModel
    {
        #region Private Members

        private readonly DataPoint[] _data;
        private readonly double[] _coefficients;

        private delegate double MEFormulaDelegate(double predictedY, double pointY);

        #endregion

        #region Constructor

        public RegressionModel(DataPoint[] data, double[] coefficients)
        {
            _data = data;
            _coefficients = coefficients;
        }

        #endregion

        public double[] Coefficients => _coefficients;

        /// <summary>
        /// Mean Absolute Error (MAE)
        /// </summary>
        public double MAE => ME(MAEFormula);

        private double MAEFormula(double predictedY, double actualY) => Math.Abs(predictedY - actualY);

        /// <summary>
        /// Mean Squared Error (MSE)
        /// </summary>
        public double MSE => ME(MSEFormula);

        private double MSEFormula(double predictedY, double actualY) => Math.Pow(predictedY - actualY, 2);

        /// <summary>
        /// Mean Error (ME)
        /// </summary>
        /// <param name="MEFormula">ME Formula</param>
        /// <returns>Error</returns>
        private double ME(MEFormulaDelegate MEFormula) 
        {
            double totalError = 0;
            for (int i=0; i<_data.Length; i++)
            {
                double predictedY = 0;
                for (int j=0; j<_coefficients.Length; j++) 
                {
                    predictedY += Math.Pow(_data[i].X, j) * _coefficients[j];
                }

                totalError += MEFormula(predictedY, _data[i].Y);
            }
            return _data.Length > 0 ? totalError / _data.Length : 0;
        }

        /// <summary>
        /// Root Mean Squared Error (RMSE)
        /// </summary>
        public double RMSE => Math.Sqrt(MSE);

    }
}
