using Core2.Selkie.Aco.Anthill.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
{
    public interface IColonyFactory : ITypedFactory
    {
        [NotNull]
        IColony Create([NotNull] IDistanceGraph graph,
                       [NotNull] IAntSettings antSettings);

        void Release([NotNull] IColony colony);
    }
}