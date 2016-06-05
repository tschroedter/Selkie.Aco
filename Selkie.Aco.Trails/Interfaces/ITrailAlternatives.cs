using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Trails.Interfaces
{
    public interface ITrailAlternatives
    {
        [NotNull]
        IEnumerable <ITrailBuilder> Trails { get; }

        void AddAlternative(int id,
                            [NotNull] ITrailBuilder trailBuilder);

        void Clear();
    }
}