using System.Collections.Generic;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common.Interfaces
{
    // todo 3-opt, LKH...
    public interface IOptimizer
    {
        [NotNull]
        [UsedImplicitly]
        IDistanceGraph DistanceGraph { set; }

        [NotNull]
        [UsedImplicitly]
        IEnumerable <int> Optimize([NotNull] IEnumerable <int> trail);
    }
}