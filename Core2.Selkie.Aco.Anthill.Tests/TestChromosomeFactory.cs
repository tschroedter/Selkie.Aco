using Core2.Selkie.Aco.Ants;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common;

namespace Core2.Selkie.Aco.Anthill.Tests
{
    internal sealed class TestChromosomeFactory : IChromosomeFactory
    {
        public IChromosome Create(double alpha,
                                  double beta,
                                  double gamma)
        {
            return new Chromosome(new SelkieRandom(),
                                  alpha,
                                  beta,
                                  gamma);
        }

        public IChromosome Create(double alpha,
                                  double beta,
                                  double gamma,
                                  double alphaMinValue,
                                  double alphaMaxValue,
                                  double betaMinValue,
                                  double betaMaxValue,
                                  double gammaMinValue,
                                  double gammaMaxValue)
        {
            return new Chromosome(new SelkieRandom(),
                                  alpha,
                                  beta,
                                  gamma,
                                  alphaMinValue,
                                  alphaMaxValue,
                                  betaMinValue,
                                  betaMaxValue,
                                  gammaMinValue,
                                  gammaMaxValue);
        }

        public void Release(IChromosome chromosome)
        {
        }
    }
}