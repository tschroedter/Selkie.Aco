using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails.Interfaces
{
    public interface ITrailHistory
    {
        IEnumerable <ITrailInformation> this[int index] { get; }

        [NotNull]
        IEnumerable <int> Lengths { get; }

        [NotNull]
        IEnumerable <ITrailInformation> Information { get; }

        [UsedImplicitly]
        int Count { get; }

        void AddTrailInformation([NotNull] ITrailBuilder trailBuilder,
                                 [NotNull] ISettings settings,
                                 int time);

        [UsedImplicitly]
        void AddTrailInformation(ITrailInformation information);

        [UsedImplicitly]
        void Clear();
    }
}