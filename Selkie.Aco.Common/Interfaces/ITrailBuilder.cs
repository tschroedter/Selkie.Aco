using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.TypedFactories;

namespace Selkie.Aco.Common.Interfaces
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