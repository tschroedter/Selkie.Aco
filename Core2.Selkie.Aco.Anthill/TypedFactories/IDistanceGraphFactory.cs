using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
{
    public interface IDistanceGraphFactory : ITypedFactory
    {
        [NotNull]
        IDistanceGraph Create([NotNull] int[][] costMatrix,
                              [NotNull] int[] costPerLine);

        void Release([NotNull] IDistanceGraph graph);
    }
}