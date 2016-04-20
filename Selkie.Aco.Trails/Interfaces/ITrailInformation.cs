using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Trails.Interfaces
{
    public interface ITrailInformation
    {
        [NotNull]
        ITrailBuilder TrailBuilder { get; }

        [NotNull]
        ISettings Settings { get; }
    }
}