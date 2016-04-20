using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface IPheromonesTrackerFactory : ITypedFactory
    {
        [NotNull]
        IPheromonesTracker Create([NotNull] IDistanceGraph graph);

        [UsedImplicitly]
        void Release([NotNull] IPheromonesTracker tracker);
    }
}