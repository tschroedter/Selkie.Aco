using JetBrains.Annotations;

namespace Selkie.Aco.Common.Interfaces
{
    public interface IPheromonesTracker
    {
        double Rho { get; }
        double Q { get; }
        double MaximumValue { get; }
        double MinimumValue { get; }
        double AverageValue { get; }
        void Clear();

        double GetValue(int indexFrom,
                        int indexTo);

        [NotNull]
        double[][] PheromonesToArray();

        void Update([NotNull] IAnt[] ants);
        void Update([NotNull] IAnt ant);
    }
}