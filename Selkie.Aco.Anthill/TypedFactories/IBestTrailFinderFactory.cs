using JetBrains.Annotations;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
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