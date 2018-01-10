using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails.Interfaces
{
    public interface ITrailAlternatives
    {
        [NotNull]
        IEnumerable <ITrailBuilder> Trails { get; }

        [UsedImplicitly]
        int Count { get; }

        void AddAlternative(int id,
                            [NotNull] ITrailBuilder trailBuilder);

        void Clear();

        [UsedImplicitly]
        bool IsKnownAlternative(ITrailBuilder trailBuilder);
    }
}