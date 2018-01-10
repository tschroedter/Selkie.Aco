using System;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Ants
{
    // todo split Chromosome into data and processor
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class Chromosome
        : IChromosome,
          IEquatable <Chromosome>
    {
        public static readonly IChromosome Unknown = new Chromosome(true);
        private readonly IRandom m_Random;

        // ReSharper disable once CodeAnnotationAnalyzer
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals(null,
                                 obj) )
            {
                return false;
            }
            if ( ReferenceEquals(this,
                                 obj) )
            {
                return true;
            }

            return obj is Chromosome chromosome && Equals(chromosome);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Alpha.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ Beta.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ Gamma.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"Alpha: {Alpha:F4} Beta: {Beta:F4} Gamma: {Gamma:F4}";
        }

        private void InitializeFactors()
        {
            if ( m_Random == null )
            {
                return;
            }

            AlphaRange = AlphaMaxValue - AlphaMinValue;
            BetaRange = BetaMaxValue - BetaMinValue;
            GammaRange = GammaMaxValue - GammaMinValue;
        }

        public Chromosome([NotNull] IRandom random)
        {
            m_Random = random;

            InitializeFactors();

            Alpha = RandomizeAlpha();
            Beta = RandomizeBeta();
            Gamma = RandomizeGamma();
        }

        private Chromosome(bool isUnknown)
        {
            IsUnknown = isUnknown;
        }

        public Chromosome([NotNull] IRandom random,
                          double alpha,
                          double beta,
                          double gamma)
        {
            AlphaMinValue = alpha - alpha * 0.1;
            AlphaMaxValue = alpha + alpha * 0.1;

            BetaMinValue = beta - beta * 0.1;
            BetaMaxValue = beta + beta * 0.1;

            GammaMinValue = gamma - gamma * 0.1;
            GammaMaxValue = gamma + gamma * 0.1;

            InitializeFactors();

            m_Random = random;
            Alpha = alpha;
            Beta = beta;
            Gamma = gamma;
        }

        [UsedImplicitly]
        public Chromosome([NotNull] IRandom random,
                          double alphaMinValue,
                          double alphaMaxValue,
                          double betaMinValue,
                          double betaMaxValue,
                          double gammaMinValue,
                          double gammaMaxValue)
        {
            m_Random = random;

            AlphaMinValue = alphaMinValue;
            AlphaMaxValue = alphaMaxValue;

            BetaMinValue = betaMinValue;
            BetaMaxValue = betaMaxValue;

            GammaMinValue = gammaMinValue;
            GammaMaxValue = gammaMaxValue;

            InitializeFactors();

            Alpha = RandomizeAlpha();
            Beta = RandomizeBeta();
            Gamma = RandomizeGamma();
        }

        // ReSharper restore TooManyDependencies

        // ReSharper disable TooManyDependencies
        public Chromosome([NotNull] IRandom random,
                          double alpha,
                          double beta,
                          double gamma,
                          double alphaMinValue,
                          double alphaMaxValue,
                          double betaMinValue,
                          double betaMaxValue,
                          double gammaMinValue,
                          double gammaMaxValue)
        {
            m_Random = random;

            AlphaMinValue = alphaMinValue;
            AlphaMaxValue = alphaMaxValue;

            BetaMinValue = betaMinValue;
            BetaMaxValue = betaMaxValue;

            GammaMinValue = gammaMinValue;
            GammaMaxValue = gammaMaxValue;

            InitializeFactors();

            Alpha = alpha;
            Beta = beta;
            Gamma = gamma;
        }

        #region IChromosome Members

        public bool IsUnknown { get; }

        public double Alpha { get; }

        public double AlphaMinValue { get; } = 0.1;

        public double AlphaMaxValue { get; } = 1.0;

        public double AlphaRange { get; private set; } = 1.0;

        public double Beta { get; }

        public double BetaMinValue { get; } = 5.0;

        public double BetaMaxValue { get; } = 7.0;

        public double BetaRange { get; private set; } = 1.0;

        public double Gamma { get; }

        public double GammaMinValue { get; } = 0.0001;

        public double GammaMaxValue { get; } = 2.0;

        public double GammaRange { get; private set; } = 1.0;

        public IChromosome Clone(IChromosomeFactory factory)
        {
            IChromosome chromosome = factory.Create(Alpha,
                                                    Beta,
                                                    Gamma,
                                                    AlphaMinValue,
                                                    AlphaMaxValue,
                                                    BetaMinValue,
                                                    BetaMaxValue,
                                                    GammaMinValue,
                                                    GammaMaxValue);

            return chromosome;
        }

        public IChromosome Randomize()
        {
            double alpha = m_Random.NextDouble() * AlphaRange + AlphaMinValue;
            double beta = m_Random.NextDouble() * BetaRange + BetaMinValue;
            double gamma = m_Random.NextDouble() * GammaRange + GammaMinValue;

            var chromosome = new Chromosome(m_Random,
                                            alpha,
                                            beta,
                                            gamma,
                                            AlphaMinValue,
                                            AlphaMaxValue,
                                            BetaMinValue,
                                            BetaMinValue,
                                            GammaMinValue,
                                            GammaMaxValue);

            return chromosome;
        }

        [UsedImplicitly]
        internal double RandomizeAlpha()
        {
            double alpha = m_Random.NextDouble() * AlphaRange + AlphaMinValue;

            return alpha;
        }

        [UsedImplicitly]
        internal double RandomizeBeta()
        {
            double beta = m_Random.NextDouble() * BetaRange + BetaMinValue;

            return beta;
        }

        [UsedImplicitly]
        internal double RandomizeGamma()
        {
            double gamma = m_Random.NextDouble() * GammaRange + GammaMinValue;

            return gamma;
        }

        #endregion

        #region IEquatable<Chromosome> Members

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(Chromosome other)
        {
            if ( ReferenceEquals(null,
                                 other) )
            {
                return false;
            }

            if ( ReferenceEquals(this,
                                 other) )
            {
                return true;
            }

            return other.Alpha.Equals(Alpha) && other.Beta.Equals(Beta) && other.Gamma.Equals(Gamma);
        }

        #endregion

        // ReSharper restore TooManyDependencies
    }
}