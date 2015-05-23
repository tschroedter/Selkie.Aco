using JetBrains.Annotations;
using Selkie.Aco.Common;

namespace Selkie.Aco.Trails
{
    public interface ITrailInformation
    {
        [NotNull]
        ITrailBuilder TrailBuilder { get; }

        [NotNull]
        ISettings Settings { get; }
    }
}