using System;
using System.Collections.Generic;
using System.Linq;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Trails.Interfaces;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Aco.Trails
{
    public class CandidateListTrailBuilder
        : BaseTrailBuilder <ICandidateListTrailBuilder>,
          ICandidateListTrailBuilder,
          IEquatable <CandidateListTrailBuilder>
    {
        // ReSharper disable once TooManyDependencies
        public CandidateListTrailBuilder([NotNull] IRandom random,
                                         [NotNull] IChromosome chromosome,
                                         [NotNull] IPheromonesTracker tracker,
                                         [NotNull] IDistanceGraph graph,
                                         [NotNull] IOptimizer optimizer)
            : base(random,
                   chromosome,
                   tracker,
                   graph,
                   optimizer)
        {
            m_NumberOfCandidates = DecideNumberOfCandidates(DistanceGraph.NumberOfNodes);
        }

        // ReSharper disable once TooManyDependencies
        [UsedImplicitly]
        public CandidateListTrailBuilder([NotNull] IRandom random,
                                         [NotNull] IChromosome chromosome,
                                         [NotNull] IPheromonesTracker tracker,
                                         [NotNull] IDistanceGraph graph,
                                         [NotNull] IOptimizer optimizer,
                                         [NotNull] IEnumerable <int> trail)
            : base(random,
                   chromosome,
                   tracker,
                   graph,
                   optimizer)
        {
            Trail = trail.ToArray();
            Length = CalculateLength(Trail);
            m_NumberOfCandidates = DecideNumberOfCandidates(DistanceGraph.NumberOfNodes);
            BuildDictionaryIndexOfTarget(Trail);
        }

        [UsedImplicitly]
        public int NumberOfCandidates
        {
            get
            {
                return m_NumberOfCandidates;
            }
            set
            {
                m_NumberOfCandidates = value <= 0 || value > DistanceGraph.NumberOfNodes
                                           ? 1
                                           : value;
            }
        }

        private int m_NumberOfCandidates;

        // ReSharper disable once CodeAnnotationAnalyzer
        public bool Equals(CandidateListTrailBuilder other)
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

            return Equals(other.Chromosome,
                          Chromosome);
        }

        [UsedImplicitly]
        internal static int DecideNumberOfCandidates(int numberOfNodes)
        {
            int number;

            switch ( numberOfNodes )
            {
                case 0:
                    number = 0;
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    number = 1;
                    break;

                default:
                    var percentage = ( int ) ( numberOfNodes * 0.25 );
                    percentage = percentage < 1
                                     ? 1
                                     : percentage;
                    number = percentage;
                    break;
            }

            return number;
        }

        [NotNull]
        [UsedImplicitly]
        internal IEnumerable <int> BuildCandidateList(int city)
        {
            // ReSharper disable once MaximumChainedReferences
            int[] neighbours = DistanceGraph.GetNeighbours(city).ToArray();

            var candidates = new int[m_NumberOfCandidates];

            for ( var i = 0 ; i < m_NumberOfCandidates ; i++ )
            {
                candidates [ i ] = neighbours [ i ];
            }

            return candidates;
        }

        [NotNull]
        [UsedImplicitly]
        internal int[] FindCandidates(int fromCity,
                                      [NotNull] bool[] visited)
        {
            IEnumerable <int> candidates = BuildCandidateList(fromCity);

            IEnumerable <int> canditatesList = candidates.Where(candidate => visited [ candidate ] == false);

            return canditatesList.ToArray();
        }

        // Attention: tested by sub-classes only
        internal override int NextCity(int cityX,
                                       [NotNull] bool[] visited,
                                       double dicider)
        {
            int[] candidates = FindCandidates(cityX,
                                              visited);

            if ( candidates.Length > 0 )
            {
                return candidates [ 0 ];
            }

            double[] probs = CalculateProbabilities(cityX,
                                                    visited);

            double[] cumul = CalculateCuMul(probs);

            for ( var i = 0 ; i < cumul.Length - 1 ; ++i )
            {
                if ( dicider >= cumul [ i ] &&
                     dicider < cumul [ i + 1 ] )
                {
                    return i;
                }
            }

            throw new ArgumentException("Failure to return valid city in NextCity");
        }
    }
}