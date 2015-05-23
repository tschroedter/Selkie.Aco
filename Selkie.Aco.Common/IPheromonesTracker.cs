using JetBrains.Annotations;

namespace Selkie.Aco.Common
{
    public interface IPheromonesTracker
    {
        double Rho { get; }
        double Q { get; }
        double MaximumValue { get; }
        double MinimumValue { get; }
        double AverageValue { get; }
        void Update([NotNull] IAnt[] ants);
        void Clear();
        void Update([NotNull] IAnt ant);

        [NotNull]
        double[][] PheromonesToArray();

        double GetValue(int indexFrom,
                        int indexTo);
    }
}