using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
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