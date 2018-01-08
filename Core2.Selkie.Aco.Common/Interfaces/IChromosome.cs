using Core2.Selkie.Aco.Common.TypedFactories;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Core2.Selkie.Aco.Common.Interfaces
{
    public interface IChromosome
    {
        bool IsUnknown { get; }
        double Alpha { get; }
        double AlphaMinValue { get; }
        double AlphaMaxValue { get; }
        double AlphaRange { get; }
        double Beta { get; }
        double BetaMinValue { get; }
        double BetaMaxValue { get; }
        double BetaRange { get; }
        double Gamma { get; }
        double GammaMinValue { get; }
        double GammaMaxValue { get; }
        double GammaRange { get; }

        [NotNull]
        IChromosome Clone([NotNull] IChromosomeFactory factory);

        [NotNull]
        IChromosome Randomize();
    }
}