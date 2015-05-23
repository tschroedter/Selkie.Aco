using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Windsor;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable once TooManyArguments

namespace Selkie.Aco.Common.TypedFactories
{
    public interface ITrailBuilderFactory : ITypedFactory
    {
        T Create <T>([NotNull] IChromosome chromosome,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IOptimizer optimizer) where T : ITrailBuilder;

        T Create <T>([NotNull] IChromosome chromosome,
                     [NotNull] IPheromonesTracker tracker,
                     [NotNull] IDistanceGraph graph,
                     [NotNull] IOptimizer optimizer,
                     [NotNull] IEnumerable <int> trail) where T : ITrailBuilder;

        [UsedImplicitly]
        void Release([NotNull] ITrailBuilder trailBuilder);
    }
}