using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Aco.Common.TypedFactories;

namespace Selkie.Aco.Common
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

        bool EdgeInTrail(int cityX,
                         int cityY);

        void Build(int start);

        [NotNull]
        ITrailBuilder Clone([NotNull] ITrailBuilderFactory trailBuilderFactory,
                            [NotNull] IChromosomeFactory chromosomeFactory);
    }
}