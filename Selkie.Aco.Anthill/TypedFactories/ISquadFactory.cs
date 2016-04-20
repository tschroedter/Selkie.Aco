using JetBrains.Annotations;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface ISquadFactory : ITypedFactory
    {
        [NotNull]
        ISquad Create([NotNull] IDistanceGraph graph,
                      [NotNull] IPheromonesTracker tracker,
                      [NotNull] IOptimizer optimizer,
                      [NotNull] IAntSettings antSettings);

        [UsedImplicitly]
        void Release([NotNull] ISquad squad);
    }
}