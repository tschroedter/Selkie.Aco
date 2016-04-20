using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Trails.Interfaces
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