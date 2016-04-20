using JetBrains.Annotations;
using Selkie.Aco.Common.TypedFactories;

namespace Selkie.Aco.Common.Interfaces
{
    public interface IAnt
    {
        [NotNull]
        ITrailBuilder TrailBuilder { get; }

        int Id { get; }

        [NotNull]
        IChromosome Chromosome { get; set; }

        string Type { get; }
        void Update();

        [NotNull]
        IAnt Clone([NotNull] IAntFactory antFactory,
                   [NotNull] IChromosomeFactory chromosomeFactory);

        void RandomizeChromosome();
    }
}