using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Core2.Selkie.Aco.Common.Interfaces;
using Core2.Selkie.Aco.Common.TypedFactories;
using Core2.Selkie.Common.Interfaces;
using JetBrains.Annotations;

[assembly: InternalsVisibleTo("Core2.Selkie.Aco.Trails.Tests")]

namespace Core2.Selkie.Aco.Trails
{
    public abstract class BaseTrailBuilder <T> : ITrailBuilder
        where T : ITrailBuilder
    {
        // ReSharper disable once TooManyDependencies
        protected BaseTrailBuilder([NotNull] IRandom random,
                                   [NotNull] IChromosome chromosome,
                                   [NotNull] IPheromonesTracker tracker,
                                   [NotNull] IDistanceGraph graph,
                                   [NotNull] IOptimizer optimizer)
        {
            Id = s_NextId++;
            Random = random;
            Chromosome = chromosome;
            m_Tracker = tracker;
            DistanceGraph = graph;
            m_Optimizer = optimizer;

            if ( DistanceGraph.NumberOfNodes == 0 ||
                 DistanceGraph.NumberOfNodes % 2 != 0 )
            {
                throw new ArgumentException("NumberOfNodes = " + DistanceGraph.NumberOfNodes);
            }

            if ( DistanceGraph.MinimumDistance <= 0.0 )
            {
                throw new ArgumentException("MinimumDistance = " + DistanceGraph.MinimumDistance);
            }

            m_TendencyMinimum = Math.Pow(m_Tracker.MinimumValue,
                                         Chromosome.Alpha) *
                                Math.Pow(Chromosome.Gamma / DistanceGraph.MaximumDistance,
                                         Chromosome.Beta);
            m_TendencyMaximum = double.MaxValue / ( graph.MinimumDistance * 100.0 );

            if ( m_TendencyMinimum < PredefinedTendencyMinimum ||
                 double.IsNaN(m_TendencyMinimum) )
            {
                m_TendencyMinimum = PredefinedTendencyMinimum;
            }
        }

        private const double PredefinedTendencyMinimum = 1E-50;
        [UsedImplicitly]
        public const int UnknownId = -1;
        // ReSharper disable once StaticMemberInGenericType
        private static int s_NextId; // means NextId per <T>

        [NotNull]
        protected IRandom Random { get; }

        [NotNull]
        protected IDistanceGraph DistanceGraph { get; }

        private readonly Dictionary <int, int> m_IndexOfTargets = new Dictionary <int, int>();
        private readonly IOptimizer m_Optimizer;
        private readonly double m_TendencyMaximum;
        private readonly double m_TendencyMinimum;
        private readonly IPheromonesTracker m_Tracker;
        private int[] m_Trail = new int[0];

        public IChromosome Chromosome { get; }

        [UsedImplicitly]
        public static int FindRelatedCity(int cityIndex)
        {
            bool isEndPoint = cityIndex % 2 == 1;

            if ( isEndPoint )
            {
                return cityIndex - 1;
            }

            return cityIndex + 1;
        }

        public static bool operator ==(BaseTrailBuilder <T> left,
                                       BaseTrailBuilder <T> right)
        {
            return Equals(left,
                          right);
        }

        public static bool operator !=(BaseTrailBuilder <T> left,
                                       BaseTrailBuilder <T> right)
        {
            return !Equals(left,
                           right);
        }

        #region IEquatable<BaseTrailBuilder> Members

        [UsedImplicitly]
        public bool Equals(BaseTrailBuilder <T> other)
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
                          Chromosome) && Equals(other.Type,
                                                  Type);
        }

        #endregion

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
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if ( obj.GetType() != typeof( BaseTrailBuilder <T> ) )
            {
                return false;
            }
            return Equals(( BaseTrailBuilder <T> ) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                int code = Chromosome != null
                               ? Chromosome.GetHashCode()
                               : 0;

                return ( code * 397 ) ^ Type.GetHashCode();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            string value = $"Length: {( int ) Length:D4}";

            sb.Append(value);
            sb.Append(" [");

            sb.Append(string.Join(" ",
                                  m_Trail));

            sb.Append("]");

            return sb.ToString();
        }

        protected void BuildDictionaryIndexOfTarget([NotNull] IEnumerable <int> trail)
        {
            m_IndexOfTargets.Clear();

            int[] trailArray = trail.ToArray();

            for ( var index = 0 ; index < trailArray.Length ; index++ )
            {
                m_IndexOfTargets.Add(trailArray [ index ],
                                     index);
            }
        }

        [NotNull]
        internal static double[] CalculateCuMul([NotNull] double[] probs)
        {
            var cumul = new double[probs.Length + 1];

            for ( var i = 0 ; i < probs.Length ; ++i )
            {
                cumul [ i + 1 ] = cumul [ i ] + probs [ i ];
            }

            return cumul;
        }

        [UsedImplicitly]
        internal virtual void BuildTrail(int startNode)
        {
            int reverseStart = FindRelatedCity(startNode);

            var trail = new int[DistanceGraph.NumberOfUniqueNodes];
            var visited = new bool[DistanceGraph.NumberOfNodes];

            trail [ 0 ] = startNode;

            visited [ startNode ] = true;
            visited [ reverseStart ] = true;

            SearchGeneral(visited,
                          trail);

            Trail = trail;
        }

        internal double CalculateLength([NotNull] IEnumerable <int> trail)
        {
            double result = DistanceGraph.Length(trail);

            return result;
        }

        [NotNull]
        internal double[] CalculateProbabilities(int cityX,
                                                 [NotNull] bool[] visited)
        {
            double[] tendency = CalculateTendencies(cityX,
                                                    visited);

            double[] probs = CreateProbabilities(tendency);

            return probs;
        }

        [UsedImplicitly]
        internal int IndexOfTarget(int target)
        {
            if ( m_IndexOfTargets.TryGetValue(target,
                                              out int index) )
            {
                return index;
            }

            return -1;
        }

        [UsedImplicitly]
        internal virtual int NextCity(int cityX,
                                      bool[] visited,
                                      double dicider)
        {
            throw new NotImplementedException();
        }

        // Attention: tested by sub-classes only
        [UsedImplicitly]
        internal void SearchGeneral([NotNull] bool[] visited,
                                    [NotNull] int[] trail)
        {
            for ( var i = 0 ; i < DistanceGraph.NumberOfUniqueNodes - 1 ; ++i )
            {
                int cityX = trail [ i ];
                double dicider = Random.NextDouble();
                int next = NextCity(cityX,
                                    visited,
                                    dicider);
                trail [ i + 1 ] = next;
                visited [ next ] = true;

                int nextRelatedCity = FindRelatedCity(next);
                visited [ nextRelatedCity ] = true;
            }
        }

        [NotNull]
        private double[] CalculateTendencies(int cityX,
                                             [NotNull] bool[] visited)
        {
            int numCities = DistanceGraph.NumberOfNodes;

            var tendency = new double[numCities];

            for ( var otherCity = 0 ; otherCity < tendency.Length ; ++otherCity )
            {
                tendency [ otherCity ] = CalculateTendency(cityX,
                                                           otherCity,
                                                           visited);
            }
            return tendency;
        }

        private double CalculateTendency(int cityX,
                                         int cityOther,
                                         [NotNull] bool[] visited)
        {
            if ( cityOther == cityX )
            {
                return 0.0;
            }

            if ( visited [ cityOther ] )
            {
                return 0.0;
            }

            // moving to an unvisited node
            double tendencyOther = Math.Pow(m_Tracker.GetValue(cityX,
                                                               cityOther),
                                            Chromosome.Alpha) * Math.Pow(Chromosome.Gamma / DistanceGraph.GetCost(cityX,
                                                                                                                cityOther),
                                                                           Chromosome.Beta);

            if ( tendencyOther < m_TendencyMinimum ||
                 double.IsNaN(tendencyOther) )
            {
                return m_TendencyMinimum;
            }

            if ( tendencyOther > m_TendencyMaximum )
            {
                tendencyOther = m_TendencyMaximum;
            }

            return tendencyOther;
        }

        [NotNull]
        private double[] CreateProbabilities([NotNull] double[] values)
        {
            int numCities = DistanceGraph.NumberOfNodes;
            var probs = new double[numCities];
            double sum = values.Sum();

            for ( var i = 0 ; i < probs.Length ; ++i )
            {
                probs [ i ] = values [ i ] / sum;
            }

            return probs;
        }

        #region ITrailBuilder Members

        public void Build(int startNode)
        {
            ValidateStartNode(startNode);
            BuildTrail(startNode); // todo better m_Trail =...

            Optimize(); // todo better m_Trail =...
            BuildIndexOfTarget(); // todo better m_Trail =...
        }

        private void Optimize()
        {
            IEnumerable <int> optimizedTrail = m_Optimizer.Optimize(m_Trail);
            m_Trail = optimizedTrail.ToArray();
            Length = CalculateLength(m_Trail);
        }

        private void ValidateStartNode(int startNode)
        {
            if ( startNode < 0 )
            {
                throw new ArgumentException($"startNode '{startNode}' is less than zero!");
            }

            if ( startNode >= DistanceGraph.NumberOfNodes )
            {
                throw new ArgumentException($"startNode '{startNode}' is equal or greater than " +
                                            $"number of graph nodes '{DistanceGraph.NumberOfNodes}'!");
            }
        }

        private void BuildIndexOfTarget()
        {
            BuildDictionaryIndexOfTarget(m_Trail);
        }

        public string Type
        {
            get
            {
                return typeof( T ).Name;
            }
        }

        [UsedImplicitly]
        public int Id { get; protected set; }

        public bool IsUnknown
        {
            get
            {
                return Id == UnknownId;
            }
        }

        public IEnumerable <int> Trail
        {
            get
            {
                return m_Trail;
            }
            protected set
            {
                m_Trail = value.ToArray();
            }
        }

        public double Length { get; protected set; } = double.MaxValue;

        // todo maybe this is not required because all of them are connected
        // ReSharper disable once MethodTooLong
        public virtual bool EdgeInTrail(int cityX,
                                        int cityY)
        {
            // are cityX and cityY adjacent to each other in trail[]?
            int idx = IndexOfTarget(cityX);

            if ( idx == -1 )
            {
                return false;
            }

            if ( idx == 0 &&
                 m_Trail [ 1 ] == cityY )
            {
                return true;
            }

            int lastIndex = m_Trail.Length - 1;

            if ( idx == 0 &&
                 m_Trail [ lastIndex ] == cityY )
            {
                return true;
            }
            if ( idx == 0 )
            {
                return false;
            }
            if ( idx == lastIndex &&
                 m_Trail [ lastIndex - 1 ] == cityY )
            {
                return true;
            }
            if ( idx == lastIndex &&
                 m_Trail [ 0 ] == cityY )
            {
                return true;
            }
            if ( idx == lastIndex )
            {
                return false;
            }
            if ( m_Trail [ idx - 1 ] == cityY )
            {
                return true;
            }
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if ( m_Trail [ idx + 1 ] == cityY )
            {
                return true;
            }

            return false;
        }

        public virtual ITrailBuilder Clone(ITrailBuilderFactory trailBuilderFactory,
                                           IChromosomeFactory chromosomeFactory)
        {
            var builder = trailBuilderFactory.Create <T>(Chromosome.Clone(chromosomeFactory),
                                                         m_Tracker,
                                                         DistanceGraph,
                                                         m_Optimizer,
                                                         Trail.ToArray());

            return builder;
        }

        #endregion
    }
}