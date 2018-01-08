using System.Collections.Generic;
using Core2.Selkie.Aco.Common.TypedFactories;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface ITrailBuilder
    {
        [NotNull]
        IEnumerable <int> Trail { get; }

        double Length { get; }
        bool IsUnknown { get; }

        [NotNull]
        string Type { get; }

        [NotNull]
        IChromosome Chromosome { get; }

        void Build(int startNode);

        [NotNull]
        ITrailBuilder Clone([NotNull] ITrailBuilderFactory trailBuilderFactory,
                            [NotNull] IChromosomeFactory chromosomeFactory);

        bool EdgeInTrail(int cityX,
                         int cityY);
    }
}