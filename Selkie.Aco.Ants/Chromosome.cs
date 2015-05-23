using System;
using JetBrains.Annotations;
using Selkie.Aco.Common;
using Selkie.Aco.Common.TypedFactories;
using Selkie.Common;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Aco.Ants
{
    // todo split Chromosome into data and processor
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class Chromosome
        : IChromosome,
          IEquatable <Chromosome>
    {
        public static readonly IChromosome Unknown = new Chromosome(true);
        private readonly double m_Alpha;
        private readonly double m_AlphaMaxValue = 1.0;
        private readonly double m_AlphaMinValue = 0.1;
        private readonly double m_Beta;
        private readonly double m_BetaMaxValue = 7.0; // reset this to 20
        private readonly double m_BetaMinValue = 5.0; // reset this to 5
        private readonly double m_Gamma;
        private readonly double m_GammaMaxValue = 2.0;
        private readonly double m_GammaMinValue = 0.0001;
        private readonly bool m_IsUnknown;
        private readonly IRandom m_Random;
        private double m_AlphaRange = 1.0;
        private double m_BetaRange = 1.0;
        private double m_GammaRange = 1.0;

        public Chromosome([NotNull] IRandom random)
        {
            m_Random = random;

            InitializeFactors();

            m_Alpha = RandomizeAlpha();
            m_Beta = RandomizeBeta();
            m_Gamma = RandomizeGamma();
        }

        private Chromosome(bool isUnknown)
        {
            m_IsUnknown = isUnknown;
        }

        public Chromosome([NotNull] IRandom random,
                          double alpha,
                          double beta,
                          double gamma)
        {
            m_AlphaMinValue = alpha - ( alpha * 0.1 );
            m_AlphaMaxValue = alpha + ( alpha * 0.1 );

            m_BetaMinValue = beta - ( beta * 0.1 );
            m_BetaMaxValue = beta + ( beta * 0.1 );

            m_GammaMinValue = gamma - ( gamma * 0.1 );
            m_GammaMaxValue = gamma + ( gamma * 0.1 );

            InitializeFactors();

            m_Random = random;
            m_Alpha = alpha;
            m_Beta = beta;
            m_Gamma = gamma;
        }

        // ReSharper disable TooManyDependencies
        public Chromosome([NotNull] IRandom random,
                          double alphaMinValue,
                          double alphaMaxValue,
                          double betaMinValue,
                          double betaMaxValue,
                          double gammaMinValue,
                          double gammaMaxValue)
        {
            m_Random = random;

            m_AlphaMinValue = alphaMinValue;
            m_AlphaMaxValue = alphaMaxValue;

            m_BetaMinValue = betaMinValue;
            m_BetaMaxValue = betaMaxValue;

            m_GammaMinValue = gammaMinValue;
            m_GammaMaxValue = gammaMaxValue;

            InitializeFactors();

            m_Alpha = RandomizeAlpha();
            m_Beta = RandomizeBeta();
            m_Gamma = RandomizeGamma();
        }

        // ReSharper restore TooManyDependencies
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
            return obj is Chromosome && Equals(( Chromosome ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = m_Alpha.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ m_Beta.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ m_Gamma.GetHashCode();
                return hashCode;
            }
        }

        private void InitializeFactors()
        {
            if ( m_Random == null )
            {
                return;
            }

            m_AlphaRange = ( m_AlphaMaxValue - m_AlphaMinValue );
            m_BetaRange = ( m_BetaMaxValue - m_BetaMinValue );
            m_GammaRange = ( m_GammaMaxValue - m_GammaMinValue );
        }

        public override string ToString()
        {
            return "Alpha: {0:F4} Beta: {1:F4} Gamma: {2:F4}".Inject(m_Alpha,
                                                                     m_Beta,
                                                                     m_Gamma);
        }

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

            m_AlphaMinValue = alphaMinValue;
            m_AlphaMaxValue = alphaMaxValue;

            m_BetaMinValue = betaMinValue;
            m_BetaMaxValue = betaMaxValue;

            m_GammaMinValue = gammaMinValue;
            m_GammaMaxValue = gammaMaxValue;

            InitializeFactors();

            m_Alpha = alpha;
            m_Beta = beta;
            m_Gamma = gamma;
        }

        #region IChromosome Members

        public bool IsUnknown
        {
            get
            {
                return m_IsUnknown;
            }
        }

        public double Alpha
        {
            get
            {
                return m_Alpha;
            }
        }

        public double AlphaMinValue
        {
            get
            {
                return m_AlphaMinValue;
            }
        }

        public double AlphaMaxValue
        {
            get
            {
                return m_AlphaMaxValue;
            }
        }

        public double AlphaRange
        {
            get
            {
                return m_AlphaRange;
            }
        }

        public double Beta
        {
            get
            {
                return m_Beta;
            }
        }

        public double BetaMinValue
        {
            get
            {
                return m_BetaMinValue;
            }
        }

        public double BetaMaxValue
        {
            get
            {
                return m_BetaMaxValue;
            }
        }

        public double BetaRange
        {
            get
            {
                return m_BetaRange;
            }
        }

        public double Gamma
        {
            get
            {
                return m_Gamma;
            }
        }

        public double GammaMinValue
        {
            get
            {
                return m_GammaMinValue;
            }
        }

        public double GammaMaxValue
        {
            get
            {
                return m_GammaMaxValue;
            }
        }

        public double GammaRange
        {
            get
            {
                return m_GammaRange;
            }
        }

        public IChromosome Clone(IChromosomeFactory factory)
        {
            IChromosome chromosome = factory.Create(m_Alpha,
                                                    m_Beta,
                                                    m_Gamma,
                                                    m_AlphaMinValue,
                                                    m_AlphaMaxValue,
                                                    m_BetaMinValue,
                                                    m_BetaMaxValue,
                                                    m_GammaMinValue,
                                                    m_GammaMaxValue);

            return chromosome;
        }

        public IChromosome Randomize()
        {
            double alpha = ( m_Random.NextDouble() * m_AlphaRange ) + m_AlphaMinValue;
            double beta = ( m_Random.NextDouble() * m_BetaRange ) + m_BetaMinValue;
            double gamma = ( m_Random.NextDouble() * m_GammaRange ) + m_GammaMinValue;

            var chromosome = new Chromosome(m_Random,
                                            alpha,
                                            beta,
                                            gamma,
                                            m_AlphaMinValue,
                                            m_AlphaMaxValue,
                                            m_BetaMinValue,
                                            m_BetaMinValue,
                                            m_GammaMinValue,
                                            m_GammaMaxValue);

            return chromosome;
        }

        internal double RandomizeAlpha()
        {
            double alpha = ( m_Random.NextDouble() * m_AlphaRange ) + m_AlphaMinValue;

            return alpha;
        }

        internal double RandomizeBeta()
        {
            double beta = ( m_Random.NextDouble() * m_BetaRange ) + m_BetaMinValue;

            return beta;
        }

        internal double RandomizeGamma()
        {
            double gamma = ( m_Random.NextDouble() * m_GammaRange ) + m_GammaMinValue;

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

            return other.m_Alpha.Equals(m_Alpha) && other.m_Beta.Equals(m_Beta) && other.m_Gamma.Equals(m_Gamma);
        }

        #endregion

        // ReSharper restore TooManyDependencies
    }
}