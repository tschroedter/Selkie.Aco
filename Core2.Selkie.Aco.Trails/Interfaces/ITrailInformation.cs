using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails.Interfaces
{
    public interface ITrailInformation
    {
        [NotNull]
        ITrailBuilder TrailBuilder { get; }

        [NotNull]
        ISettings Settings { get; }

        [UsedImplicitly]
        int Time { get; }
    }
}