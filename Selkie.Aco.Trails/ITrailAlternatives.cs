using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common;

namespace Selkie.Aco.Trails
{
    public interface ITrailAlternatives
    {
        [NotNull]
        IEnumerable <ITrailBuilder> Trails { get; }

        void Clear();

        void AddAlternative(int id,
                            [NotNull] ITrailBuilder trailBuilder);
    }
}