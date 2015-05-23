using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Aco.Common
{
    public interface INearestNeighbours
    {
        bool IsUnknown { get; }
        void Initialize([NotNull] int[][] costMatrix);

        [NotNull]
        IEnumerable <int> GetNeighbours(int index);
    }
}