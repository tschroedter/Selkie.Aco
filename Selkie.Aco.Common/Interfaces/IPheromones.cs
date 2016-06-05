using JetBrains.Annotations;

namespace Selkie.Aco.Common.Interfaces
{
    public interface IPheromones
    {
        int NumberOfNodes { get; }

        double CalculateAverageValue();

        double GetValue(int fromIndex,
                        int toIndex);

        void Initialize([NotNull] InitializeInformation information);

        void SetValue(int fromIndex,
                      int toIndex,
                      double value);

        [NotNull]
        double[][] ToArray();

        void UpdateForAnt([NotNull] IAnt ant,
                          int fromIndex,
                          int toIndex);
    }
}