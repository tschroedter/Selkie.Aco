using System;
using Core2.Selkie.Aco.Ants.Interfaces;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Ants
{
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class Crossover : ICrossover
    {
        public Crossover([NotNull] IDisposer disposer,
                         [NotNull] ISelkieLogger logger,
                         [NotNull] IRandom random,
                         [NotNull] IChromosomeFactory factory)
        {
            Disposer = disposer;
            m_Logger = logger;
            m_Random = random;
            m_Factory = factory;
        }

        [UsedImplicitly]
        public const int NumberOfGenes = 3;

        [UsedImplicitly]
        public const bool FromFather = true;

        [UsedImplicitly]
        public const bool FromMother = false;

        [UsedImplicitly]
        internal IDisposer Disposer { private get; set; }

        private readonly IChromosomeFactory m_Factory;
        private readonly ISelkieLogger m_Logger;
        private readonly IRandom m_Random;

        public IChromosome Mutation(IChromosome chromosome)
        {
            int randomGene = m_Random.Next(0,
                                           NumberOfGenes * 2);

            IChromosome mutation = Mutation(chromosome,
                                            ( Gene ) randomGene);

            return mutation;
        }

        public IChromosome Offspring(IChromosome male,
                                     IChromosome female)
        {
            bool[] genes = CreateGenes();

            return Offspring(male,
                             female,
                             genes);
        }

        public void Dispose()
        {
            Disposer.Dispose();
        }

        [NotNull]
        [UsedImplicitly]
        internal bool[] CreateGenes()
        {
            var genes = new bool[NumberOfGenes];

            for ( var i = 0 ; i < NumberOfGenes ; i++ )
            {
                genes [ i ] = m_Random.NextDouble() < 0.5;
            }

            return genes;
        }

        [NotNull]
        [UsedImplicitly]
        internal IChromosome Mutation([NotNull] IChromosome chromosome,
                                      Gene random)
        {
            IChromosome mutation;

            switch ( random )
            {
                case Gene.Alpha:
                {
                    mutation = m_Factory.Create(m_Random.NextDouble() * chromosome.AlphaRange,
                                                chromosome.Beta,
                                                chromosome.Gamma);
                    break;
                }
                case Gene.Beta:
                {
                    mutation = m_Factory.Create(chromosome.Alpha,
                                                m_Random.NextDouble() * chromosome.BetaRange,
                                                chromosome.Gamma);
                    break;
                }
                case Gene.Gamma:
                {
                    mutation = m_Factory.Create(chromosome.Alpha,
                                                chromosome.Beta,
                                                m_Random.NextDouble() * chromosome.GammaRange);
                    break;
                }
                default:
                {
                    m_Logger.Error($"Unknown cross over gene id '{random}'! - Using clone instead!");

                    return chromosome.Clone(m_Factory);
                }
            }

            Disposer.AddResource(() => m_Factory.Release(mutation));

            return mutation;
        }

        [NotNull]
        [UsedImplicitly]
        internal IChromosome Offspring([NotNull] IChromosome male,
                                       [NotNull] IChromosome female,
                                       [NotNull] bool[] genes)
        {
            double averageAlpha = ( male.Alpha + female.Alpha ) / 2.0;
            double averageBeta = ( male.Beta + female.Beta ) / 2.0;
            double averageGamma = ( male.Gamma + female.Gamma ) / 2.0;

            double deltaAlpha = Math.Abs(male.Alpha - female.Alpha);
            double deltaBeta = Math.Abs(male.Beta - female.Beta);
            double deltaGamma = Math.Abs(male.Gamma - female.Gamma);

            double randomAlpha = m_Random.NextDouble() * deltaAlpha;
            double randomBeta = m_Random.NextDouble() * deltaBeta;
            double randomGamma = m_Random.NextDouble() * deltaGamma;

            double alpha = genes [ 0 ]
                               ? averageAlpha + randomAlpha
                               : averageAlpha - randomAlpha;
            double beta = genes [ 1 ]
                              ? averageBeta + randomBeta
                              : averageBeta - randomBeta;
            double gamma = genes [ 2 ]
                               ? averageGamma + randomGamma
                               : averageGamma - randomGamma;

            double alphaMinValue = male.AlphaMinValue < female.AlphaMinValue
                                       ? male.AlphaMinValue
                                       : female.AlphaMinValue;
            double alphaMaxValue = male.AlphaMaxValue < female.AlphaMaxValue
                                       ? male.AlphaMaxValue
                                       : female.AlphaMaxValue;

            double betaMinValue = male.BetaMinValue < female.BetaMinValue
                                      ? male.BetaMinValue
                                      : female.BetaMinValue;
            double betaMaxValue = male.BetaMaxValue < female.BetaMaxValue
                                      ? male.BetaMaxValue
                                      : female.BetaMaxValue;

            double gammaMinValue = male.GammaMinValue < female.GammaMinValue
                                       ? male.GammaMinValue
                                       : female.GammaMinValue;
            double gammaMaxValue = male.GammaMaxValue < female.GammaMaxValue
                                       ? male.GammaMaxValue
                                       : female.GammaMaxValue;

            IChromosome chromosome = m_Factory.Create(alpha,
                                                      beta,
                                                      gamma,
                                                      alphaMinValue,
                                                      alphaMaxValue,
                                                      betaMinValue,
                                                      betaMaxValue,
                                                      gammaMinValue,
                                                      gammaMaxValue);

            Disposer.AddResource(() => m_Factory.Release(chromosome));

            return chromosome;
        }

        #region Nested type: Gene

        internal enum Gene
        {
            Alpha,
            Beta,
            Gamma
        }

        #endregion
    }
}