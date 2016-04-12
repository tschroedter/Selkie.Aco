using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.Aco.Common
{
    public interface IAntFactory : ITypedFactory
    {
        // ReSharper disable once TooManyArguments
        [NotNull]
        T Create <T>([NotNull] IChromosome chromosome,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IOptimizer optimizer,
                     [NotNull] IAntSettings antSettings,
                     [NotNull] IEnumerable <int> trail) where T : IAnt;

        void Release([NotNull] IAnt ant);
    }
}