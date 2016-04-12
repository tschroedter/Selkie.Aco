using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface IColonyFactory : ITypedFactory
    {
        [NotNull]
        IColony Create([NotNull] IDistanceGraph graph,
                       [NotNull] IAntSettings antSettings);

        void Release([NotNull] IColony colony);
    }
}