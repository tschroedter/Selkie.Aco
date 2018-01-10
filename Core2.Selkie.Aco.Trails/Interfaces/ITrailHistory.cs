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

        void AddTrailInformation([NotNull] ITrailBuilder trailBuilder,
                                 [NotNull] ISettings settings,
                                 int time);
    }
}