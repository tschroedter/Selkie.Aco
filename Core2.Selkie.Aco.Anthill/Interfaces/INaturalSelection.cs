using Core2.Selkie.Aco.Trails.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.Interfaces
{
    public interface INaturalSelection
    {
        [NotNull]
        ITrailHistory TrailHistory { get; }

        void DoSelection();
    }
}