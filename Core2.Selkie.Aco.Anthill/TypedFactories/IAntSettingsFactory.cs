using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.TypedFactories
{
    public interface IAntSettingsFactory : ITypedFactory
    {
        [NotNull]
        IAntSettings Create(AntSettings.TrailStartNodeType trailStartNodeType,
                            int fixedStartNode);

        void Release([NotNull] IAntSettings antSettings);
    }
}