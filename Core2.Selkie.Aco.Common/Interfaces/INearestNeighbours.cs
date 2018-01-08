using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface INearestNeighbours
    {
        [UsedImplicitly]
        bool IsUnknown { get; }

        [NotNull]
        IEnumerable <int> GetNeighbours(int index);

        void Initialize([NotNull] int[][] costMatrix);
    }
}