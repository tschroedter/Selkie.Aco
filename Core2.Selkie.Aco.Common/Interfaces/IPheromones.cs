using JetBrains.Annotations;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface IPheromones
    {
        double InitialValue { get; }
        double MinimumValue { get; }
        double MaximumValue { get; }

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