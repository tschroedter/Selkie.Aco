using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
{
    public interface IBestTrailFinderFactory : ITypedFactory
    {
        [NotNull]
        IBestTrailFinder Create([NotNull] IDistanceGraph graph,
                                [NotNull] IPheromonesTracker tracker,
                                [NotNull] IOptimizer optimizer);

        [UsedImplicitly]
        void Release([NotNull] IBestTrailFinder bestTrailFinder);
    }
}