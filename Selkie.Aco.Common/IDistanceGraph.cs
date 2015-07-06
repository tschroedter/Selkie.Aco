using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Aco.Common
{
    public interface IDistanceGraph
    {
        int NumberOfNodes { get; }
        int NumberOfUniqueNodes { get; }
        double AverageDistance { get; }
        double MinimumDistance { get; }
        double MaximumDistance { get; }
        bool IsUnknown { get; }

        int GetCost(int fromIndex,
                    int toIndex);

        double Length([NotNull] IEnumerable <int> trail);

        [NotNull]
        IEnumerable <int> GetNeighbours(int node);

        bool IsValidPath([NotNull] IEnumerable <int> trail);

        bool IsValid();
    }
}