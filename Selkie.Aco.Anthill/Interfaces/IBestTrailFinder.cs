using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.Interfaces;

namespace Selkie.Aco.Anthill.Interfaces
{
    public interface IBestTrailFinder : IDisposable
    {
        [NotNull]
        ISettings Settings { get; }

        [NotNull]
        IEnumerable <ITrailBuilder> AlternativeTrails { get; }

        [NotNull]
        ITrailBuilder BestTrailBuilder { get; }

        [NotNull]
        IAnt BestAnt { get; }

        void FindBestTrail([NotNull] IEnumerable <IAnt> ants);
        void Clear();
    }
}