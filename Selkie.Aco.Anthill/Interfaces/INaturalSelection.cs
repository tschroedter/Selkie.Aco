using JetBrains.Annotations;
using Selkie.Aco.Trails.Interfaces;

namespace Selkie.Aco.Anthill.Interfaces
{
    public interface INaturalSelection
    {
        [NotNull]
        ITrailHistory TrailHistory { get; }

        void DoSelection();
    }
}