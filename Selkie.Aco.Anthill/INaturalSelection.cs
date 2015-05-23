using JetBrains.Annotations;
using Selkie.Aco.Trails;

namespace Selkie.Aco.Anthill
{
    public interface INaturalSelection
    {
        [NotNull]
        ITrailHistory TrailHistory { get; }

        void DoSelection();
    }
}