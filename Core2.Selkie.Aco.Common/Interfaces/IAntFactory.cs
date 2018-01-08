using System.Collections.Generic;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface IAntFactory : ITypedFactory
    {
        [NotNull]
        [UsedImplicitly]
        T Create <T>([NotNull] IChromosome chromosome,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IOptimizer optimizer,
                     [NotNull] IAntSettings antSettings,
                     [NotNull] IEnumerable <int> trail)
            where T : IAnt;

        [UsedImplicitly]
        void Release([NotNull] IAnt ant);
    }
}