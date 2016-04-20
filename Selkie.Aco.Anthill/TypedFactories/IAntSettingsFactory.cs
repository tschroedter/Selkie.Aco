using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;
using Selkie.Windsor;

namespace Selkie.Aco.Anthill.TypedFactories
{
    public interface IAntSettingsFactory : ITypedFactory
    {
        [NotNull]
        IAntSettings Create(AntSettings.TrailStartNodeType trailStartNodeType,
                            int fixedStartNode);

        void Release([NotNull] IAntSettings antSettings);
    }
}