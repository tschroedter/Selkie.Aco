using JetBrains.Annotations;

namespace Selkie.Aco.Common
{
    public interface IPheromones
    {
        int NumberOfNodes { get; }

        void SetValue(int fromIndex,
                      int toIndex,
                      double value);

        double GetValue(int fromIndex,
                        int toIndex);

        double CalculateAverageValue();

        [NotNull]
        double[][] ToArray();

        void UpdateForAnt([NotNull] IAnt ant,
                          int fromIndex,
                          int toIndex);

        void Initialize([NotNull] InitializeInformation information);
    }
}