using System;
using System.Collections.Generic;
using Core2.Selkie.Aco.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Anthill.Interfaces
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

        void Clear();

        void FindBestTrail([NotNull] IEnumerable <IAnt> ants);
    }
}