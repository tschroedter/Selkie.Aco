using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
{
    public interface IPheromonesTrackerFactory : ITypedFactory
    {
        [NotNull]
        IPheromonesTracker Create([NotNull] IDistanceGraph graph);

        [UsedImplicitly]
        void Release([NotNull] IPheromonesTracker tracker);
    }
}