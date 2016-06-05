using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Aco.Common.Interfaces
{
    public interface INearestNeighbours
    {
        bool IsUnknown { get; }

        [NotNull]
        IEnumerable <int> GetNeighbours(int index);

        void Initialize([NotNull] int[][] costMatrix);
    }
}