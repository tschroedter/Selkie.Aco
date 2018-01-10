using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails.Interfaces
{
    public interface ICandidateListTrailBuilder : ITrailBuilder
    {
        [UsedImplicitly]
        int NumberOfCandidates { get; set; }
    }
}