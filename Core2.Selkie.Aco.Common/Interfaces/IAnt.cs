using Core2.Selkie.Aco.Common.TypedFactories;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface IAnt
    {
        [NotNull]
        ITrailBuilder TrailBuilder { get; }

        int Id { get; }

        [NotNull]
        IChromosome Chromosome { get; set; }

        string Type { get; }
        double Alpha { get; }
        double Beta { get; }
        double Gamma { get; }

        [NotNull]
        IAnt Clone([NotNull] IAntFactory antFactory,
                   [NotNull] IChromosomeFactory chromosomeFactory);

        void RandomizeChromosome();
        void Update();
    }
}