using System.Collections.Generic;
using JetBrains.Annotations;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Common.Interfaces
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

        [NotNull]
        IEnumerable <int> GetNeighbours(int node);

        bool IsValid();

        bool IsValidPath([NotNull] IEnumerable <int> trail);

        double Length([NotNull] IEnumerable <int> trail);
    }
}