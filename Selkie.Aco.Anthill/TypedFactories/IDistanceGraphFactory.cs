using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface IDistanceGraphFactory : ITypedFactory
    {
        [NotNull]
        IDistanceGraph Create([NotNull] int[][] costMatrix,
                              [NotNull] int[] costPerLine);

        void Release([NotNull] IDistanceGraph graph);
    }
}