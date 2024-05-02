using Statistical.Algorithms.Models;
using Statistical.Algorithms.Structs;

namespace Statistical.Algorithms.Interfaces
{
    public interface IRegressionStrategy
    {
        void Fit(DataPoint[] data);

        double[] Predict(double[] xValues);

        RegressionModel? Model { get; }
    }
}
