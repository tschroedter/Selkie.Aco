using System.Collections.Generic;
using JetBrains.Annotations;

namespace Selkie.Aco.Common
{
    // todo 3-opt, LKH...
    public interface IOptimizer
    {
        [NotNull]
        IDistanceGraph DistanceGraph { set; }

        [NotNull]
        IEnumerable <int> Optimize([NotNull] IEnumerable <int> trail);
    }
}