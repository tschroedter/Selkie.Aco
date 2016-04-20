using JetBrains.Annotations;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface IQueenFactory : ITypedFactory
    {
        [NotNull]
        IQueen Create([NotNull] IDistanceGraph graph,
                      [NotNull] IPheromonesTracker tracker,
                      [NotNull] IOptimizer optimizer,
                      [NotNull] IAntSettings antSettings);

        [UsedImplicitly]
        void Release([NotNull] IQueen queen);
    }
}