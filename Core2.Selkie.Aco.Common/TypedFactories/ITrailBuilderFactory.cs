using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Common.TypedFactories
{
    public interface ITrailBuilderFactory : ITypedFactory
    {
        [UsedImplicitly]
        T Create <T>([NotNull] IChromosome chromosome,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IOptimizer optimizer)
            where T : ITrailBuilder;

        [UsedImplicitly]
        T Create <T>([NotNull] IChromosome chromosome,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IOptimizer optimizer,
                     [NotNull] IEnumerable <int> trail)
            where T : ITrailBuilder;

        [UsedImplicitly]
        void Release([NotNull] ITrailBuilder trailBuilder);
    }
}